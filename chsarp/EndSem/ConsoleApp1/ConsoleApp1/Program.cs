// ================================================================
// Program.cs (Composition Root) — 콘솔 의존성 주입 & 게임 실행
// ================================================================
using System;
using ShootingGameLib;

public static class Program
{
    public static void Main(string[] args)
    {
        // 인프라 구현체 준비 (콘솔 기반)
        IInputProvider input = new ConsoleInputProvider();
        IRenderer renderer = new ConsoleRenderer(width: 100, height: 40);

        // 게임 조립 (도메인/애플리케이션 계층)
        var game = new Game(input, renderer);
        game.Run();
    }
}


// ================================================================
// ShootingGameLib.cs (도메인 + 애플리케이션 + 인프라 추상화)
//  - 핵심 변경점
//    1) GameLoop (입력→업데이트→렌더) 도입
//    2) 상태(State) 패턴으로 메뉴/플레이어 테스트/적 테스트 분리
//    3) IInputProvider, IRenderer 인터페이스로 System.Console 의존 분리
//    4) Position/Entity 등 도메인 모델 정립
//    5) 기존 기능(플레이어/적 테스트, 목록/추가/삭제/레벨변경) 유지
// ================================================================

namespace ShootingGameLib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Threading;

    // -----------------------------
    // 1) 도메인 모델
    // -----------------------------

    // 좌표: 값 타입 + 불변
    public readonly record struct Position(int X, int Y)
    {
        public static Position Zero => new(0, 0);
        public Position Translate(int dx, int dy) => new(X + dx, Y + dy);
        public static Position operator +(Position a, Position b) => new(a.X + b.X, a.Y + b.Y);
    }

    public enum PlayerType { P1, P2 }
    public enum EnemyType { Normal, MidBoss, FinalBoss }

    // 공통 게임 오브젝트
    public abstract class Entity(Position position, char symbol, ConsoleColor color)
    {
        public Position Position { get; protected set; } = position;
        public char Symbol { get; } = symbol;
        public ConsoleColor Color { get; } = color;
        public void Teleport(Position p) => Position = p;
        public abstract void Update(GameContext context);
        public virtual void Draw(IRenderer renderer) => renderer.Draw(Position, Symbol, Color);
    }

    // 능력치/레벨 보유 유닛
    public abstract class Actor(Position position, char symbol, ConsoleColor color,
                               string name, int baseHp, int baseAtk)
        : Entity(position, symbol, color)
    {
        public string Name { get; protected set; } = name;
        public int Level { get; protected set; } = 1;
        public int BaseHp { get; protected set; } = baseHp;
        public int BaseAttack { get; protected set; } = baseAtk;
        public int BonusHp { get; protected set; } = 0;
        public int BonusAttack { get; protected set; } = 0;

        public int CurrentHp => BaseHp + (Level * 10) + BonusHp;
        public int CurrentAttack => BaseAttack + (Level * 2) + BonusAttack;
        public void SetLevel(int level) => Level = Math.Max(1, level);
    }

    public class Player : Actor
    {
        public PlayerType Type { get; }

        public Player(string name, PlayerType type)
            : base(position: type == PlayerType.P1 ? new Position(10, 20) : new Position(30, 20),
                   symbol: '@', color: ConsoleColor.Green,
                   name: name, baseHp: 100, baseAtk: 10)
        {
            Type = type;
        }

        public override void Update(GameContext context)
        {
            // 플레이어는 프레임마다 입력 이벤트를 소모해 이동/공격
            foreach (var key in context.FrameKeys)
            {
                // P1: WASD/F/G, P2: IJKL/;/' (원 코드 매핑 유지)
                if (Type == PlayerType.P1)
                {
                    switch (key)
                    {
                        case ConsoleKey.W: Position = Position.Translate(0, -1); context.Log($"[{Name}] 위로 이동"); break;
                        case ConsoleKey.S: Position = Position.Translate(0, +1); context.Log($"[{Name}] 아래로 이동"); break;
                        case ConsoleKey.A: Position = Position.Translate(-1, 0); context.Log($"[{Name}] 왼쪽으로 이동"); break;
                        case ConsoleKey.D: Position = Position.Translate(+1, 0); context.Log($"[{Name}] 오른쪽으로 이동"); break;
                        case ConsoleKey.F: context.Log($"[{Name}] 미사일 발사! (Atk: {CurrentAttack})"); break;
                        case ConsoleKey.G: context.Log($"[{Name}] 필살기 사용!"); break;
                    }
                }
                else // P2
                {
                    switch (key)
                    {
                        case ConsoleKey.I: Position = Position.Translate(0, -1); context.Log($"[{Name}] 위로 이동"); break;
                        case ConsoleKey.K: Position = Position.Translate(0, +1); context.Log($"[{Name}] 아래로 이동"); break;
                        case ConsoleKey.J: Position = Position.Translate(-1, 0); context.Log($"[{Name}] 왼쪽으로 이동"); break;
                        case ConsoleKey.L: Position = Position.Translate(+1, 0); context.Log($"[{Name}] 오른쪽으로 이동"); break;
                        case ConsoleKey.Oem1: context.Log($"[{Name}] 미사일 발사! (Atk: {CurrentAttack})"); break;
                        case ConsoleKey.Oem7: context.Log($"[{Name}] 필살기 사용!"); break;
                    }
                }
            }
        }
    }

    public class Enemy : Actor
    {
        public int SpawnId { get; internal set; }
        public int SpawnOrder { get; internal set; }
        public string Pattern { get; internal set; } = string.Empty;
        public EnemyType Type { get; }

        public Enemy(string name, int hp, int atk, EnemyType type)
            : base(position: new Position(50, 10), symbol: 'E', color: ConsoleColor.Red,
                   name: name, baseHp: hp, baseAtk: atk)
        {
            Type = type;
        }

        public override void Update(GameContext context)
        {
            // 간단한 데모: Normal은 아래로, MidBoss는 좌우 왕복, FinalBoss는 제자리
            Position next = Type switch
            {
                EnemyType.Normal => Position.Translate(0, +1),
                EnemyType.MidBoss => Position.Translate(context.Tick % 2 == 0 ? +1 : -1, 0),
                _ => Position
            };
            Position = next;
        }
    }

    public static class EnemyFactory
    {
        private static int _idCounter = 1;
        public static Enemy Create(EnemyType type, int order, int x, int y)
        {
            Enemy enemy = type switch
            {
                EnemyType.Normal => new Enemy("잡몹", 50, 5, type) { Pattern = "직진 하강" },
                EnemyType.MidBoss => new Enemy("중보", 200, 15, type) { Pattern = "좌우 왕복, 공격" },
                EnemyType.FinalBoss => new Enemy("막보", 1000, 50, type) { Pattern = "순간 돌진, 총알 난사" },
                _ => new Enemy("잡몹", 50, 5, EnemyType.Normal)
            };

            enemy.SpawnId = _idCounter++;
            enemy.SpawnOrder = order;
            enemy.Teleport(new Position(x, y));
            return enemy;
        }
    }

    public class StageManager
    {
        public List<Enemy> EnemyList { get; } = new();
        public void AddEnemy(Enemy enemy) => EnemyList.Add(enemy);
        public void InsertEnemy(int index, Enemy enemy)
        { if (index >= 0 && index <= EnemyList.Count) EnemyList.Insert(index, enemy); else EnemyList.Add(enemy); }
        public void RemoveEnemyById(int id)
        { var t = EnemyList.FirstOrDefault(e => e.SpawnId == id); if (t != null) EnemyList.Remove(t); }
    }

    // -----------------------------
    // 2) 인프라 추상화 (입출력)
    // -----------------------------

    public interface IInputProvider
    {
        bool TryReadKey(out ConsoleKey key); // 논블로킹
        string ReadLine();                   // 블로킹 (필요 시)
        void ClearKeyBuffer();
    }

    public interface IRenderer
    {
        void Clear();
        void Draw(Position position, char symbol, ConsoleColor color);
        void Write(string text);
        void WriteLine(string text = "");
        void SetCursorPosition(int x, int y);
        void Render();
    }

    // 콘솔 구현체 (인프라)
    public class ConsoleInputProvider : IInputProvider
    {
        public bool TryReadKey(out ConsoleKey key)
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).Key;
                return true;
            }
            key = default;
            return false;
        }
        public string ReadLine() => Console.ReadLine() ?? string.Empty;
        public void ClearKeyBuffer() { while (Console.KeyAvailable) Console.ReadKey(true); }
    }

    public class ConsoleRenderer : IRenderer
    {
        public ConsoleRenderer(int width = 100, int height = 40)
        {
            try
            {
                Console.Clear();
                Console.CursorVisible = false;
                if (OperatingSystem.IsWindows())
                {
                    Console.SetWindowSize(Math.Max(20, width), Math.Max(10, height));
                    Console.SetBufferSize(Math.Max(20, width), Math.Max(10, height));
                }
            }
            catch { /* 터미널에 따라 실패 가능 — 무시 */ }
        }
        public void Clear() { try { Console.Clear(); } catch { } }
        public void Draw(Position position, char symbol, ConsoleColor color)
        {
            try
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(Math.Max(0, position.X), Math.Max(0, position.Y));
                Console.Write(symbol);
                Console.ResetColor();
            }
            catch { }
        }
        public void Write(string text) { Console.Write(text); }
        public void WriteLine(string text = "") { Console.WriteLine(text); }
        public void SetCursorPosition(int x, int y)
        { try { Console.SetCursorPosition(Math.Max(0, x), Math.Max(0, y)); } catch { } }
        public void Render() { /* 콘솔 즉시모드 — 별도 버퍼 flush 없음 */ }
    }

    // -----------------------------
    // 3) 게임 루프 & 상태
    // -----------------------------

    public class GameContext
    {
        public ImmutableArray<ConsoleKey> FrameKeys { get; internal set; } = ImmutableArray<ConsoleKey>.Empty;
        public int Tick { get; internal set; }
        public List<Player> Players { get; } = new();
        public StageManager Stage { get; } = new();
        public IRenderer Renderer { get; }
        public IInputProvider Input { get; }

        // 간단한 순환 로그 버퍼
        private readonly Queue<string> _screenLog = new();
        private const int MaxScreenLogLines = 15;
        public IEnumerable<string> ScreenLogs => _screenLog.ToArray();

        public GameContext(IInputProvider input, IRenderer renderer)
        {
            Input = input; Renderer = renderer;
            Players.Add(new Player("Player1", PlayerType.P1));
            // 초기 적 생성 (원 코드 로직 유지)
            for (int i = 0; i < 20; i++)
            {
                int order = i + 1;
                var type = order == 20 ? EnemyType.FinalBoss : (order % 5 == 0 ? EnemyType.MidBoss : EnemyType.Normal);
                int x = (i % 5) * 50 + 50;
                Stage.AddEnemy(EnemyFactory.Create(type, order, x, 100));
            }
        }

        public void Log(string msg)
        {
            var line = $"[{DateTime.Now:HH:mm:ss}] {msg}";
            _screenLog.Enqueue(line);
            while (_screenLog.Count > MaxScreenLogLines) _screenLog.Dequeue();
        }
    }

    public interface IGameState
    {
        void Update(Game game);
        void Render(Game game);
    }

    public class Game
    {
        private readonly IInputProvider _input;
        private readonly IRenderer _renderer;
        private bool _running = true;

        public GameContext Context { get; }
        public IGameState State { get; private set; }

        public Game(IInputProvider input, IRenderer renderer)
        {
            _input = input; _renderer = renderer;
            Context = new GameContext(input, renderer);
            State = new MenuState();
        }

        public void SetState(IGameState next) => State = next;
        public void Exit() => _running = false;

        public void Run()
        {
            var keys = new List<ConsoleKey>(capacity: 4);

            while (_running)
            {
                // 1) 입력 수집 (논블로킹)
                keys.Clear();
                while (_input.TryReadKey(out var key)) keys.Add(key);
                Context.FrameKeys = keys.ToImmutableArray();

                // 2) 업데이트
                State.Update(this);

                // 3) 렌더링
                _renderer.Clear();
                State.Render(this);
                _renderer.Render();

                // 4) 틱 진행 + 프레임 타이밍
                Context.Tick++;
                Thread.Sleep(16); // ~60 FPS
            }
        }
    }

    // -----------------------------
    // 3-1) 메뉴 상태
    // -----------------------------

    public class MenuState : IGameState
    {
        public void Update(Game game)
        {
            foreach (var key in game.Context.FrameKeys)
            {
                switch (key)
                {
                    case ConsoleKey.D1: game.SetState(new PlayerTestState()); return;
                    case ConsoleKey.D2: game.SetState(new EnemyLogState()); return;
                    case ConsoleKey.D3: /* 강제 리셋 */ ConsoleReset(); return;
                    case ConsoleKey.Escape: game.Exit(); return;
                }
            }
        }

        public void Render(Game game)
        {
            var r = game.Context.Renderer;
            r.WriteLine("============ 슈팅게임 모니터 ============");
            r.WriteLine("1. 플레이어 기체 테스트");
            r.WriteLine("2. 적 객체 동작 테스트 (로그 모드)");
            r.WriteLine("3. 콘솔 화면 초기화 (강제 리셋)");
            r.WriteLine("\nESC. 프로그램 종료");
            r.WriteLine("=========================================");
            r.Write("메뉴 선택: ");
        }

        private static void ConsoleReset()
        {
            try
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                if (OperatingSystem.IsWindows())
                {
                    Console.SetWindowSize(100, 40);
                    Console.SetBufferSize(100, 40);
                }
                Console.CursorVisible = false;
            }
            catch { }
        }
    }

    // -----------------------------
    // 3-2) 플레이어 테스트 상태
    // -----------------------------

    public class PlayerTestState : IGameState
    {
        public void Update(Game game)
        {
            var ctx = game.Context;

            foreach (var key in ctx.FrameKeys)
            {
                if (key == ConsoleKey.Escape) { game.SetState(new MenuState()); return; }
                else if (key == ConsoleKey.D1)
                {
                    if (ctx.Players.Count < 2)
                    { ctx.Players.Add(new Player("Player2", PlayerType.P2)); ctx.Log("[System] Player2 추가됨 (IJKL/;/' )"); }
                    else ctx.Log("[System] 이미 2인 플레이 중입니다.");
                }
                else if (key == ConsoleKey.D2)
                {
                    // 레벨 변경 프로시저 (필요 시 블로킹 입력 사용)
                    var r = ctx.Renderer; r.WriteLine(); r.Write(" [레벨] 변경할 레벨 값 입력 : ");
                    var line = ctx.Input.ReadLine();
                    if (int.TryParse(line, out int lv))
                    { foreach (var p in ctx.Players) p.SetLevel(lv); ctx.Log($"[Level] 모든 플레이어 레벨 {lv}(으)로 변경."); }
                    else ctx.Log("[Error] 잘못된 숫자 입력입니다.");
                }
            }

            // 입력에 반응하여 플레이어 업데이트
            foreach (var p in ctx.Players) p.Update(ctx);
        }

        public void Render(Game game)
        {
            var ctx = game.Context; var r = ctx.Renderer;
            r.WriteLine("============ [플레이어 동작 로그] ============");
            foreach (var line in ctx.ScreenLogs) r.WriteLine(" " + line);
            // 빈 줄 채우기 (고정 높이 느낌)
            int filled = ctx.ScreenLogs.Count();
            for (int i = filled; i < 15; i++) r.WriteLine();
            r.WriteLine("==============================================");
            r.WriteLine(" [현재 상태]");
            foreach (var p in ctx.Players)
                r.WriteLine($" ▶ {p.Name,-7} : HP {p.CurrentHp,3} / ATK {p.CurrentAttack,2} / Pos ({p.Position.X,2}, {p.Position.Y,2})");
            r.WriteLine(" [WASD] 1P 조작   [IJKL] 2P 조작");
            r.WriteLine("-----------------------------------------------");
            r.WriteLine(" 1. 플레이어 추가 (2P)");
            r.WriteLine(" 2. 플레이어 레벨 변경 (전체)");
            r.WriteLine(" ESC. 뒤로 가기");
            r.WriteLine("-----------------------------------------------");
            r.Write(" 명령 대기 >> ");
        }
    }

    // -----------------------------
    // 3-3) 적 테스트(로그) 상태
    // -----------------------------

    public class EnemyLogState : IGameState
    {
        public void Update(Game game)
        {
            var ctx = game.Context;
            foreach (var key in ctx.FrameKeys)
            {
                if (key == ConsoleKey.Escape) { game.SetState(new MenuState()); return; }
                switch (key)
                {
                    case ConsoleKey.D1: ShowEnemyListView(ctx); break;
                    case ConsoleKey.D2: InputAddEnemy(ctx); break;
                    case ConsoleKey.D3: InputDeleteEnemy(ctx); break;
                    case ConsoleKey.D4: InputLevelChange(ctx); break;
                }
            }

            // 프레임 업데이트: 적 움직임 등
            foreach (var e in ctx.Stage.EnemyList) e.Update(ctx);
        }

        public void Render(Game game)
        {
            var ctx = game.Context; var r = ctx.Renderer;
            r.WriteLine("============ [적 동작 로그 모니터] ============");
            foreach (var line in ctx.ScreenLogs) r.WriteLine(" " + line);
            int filled = ctx.ScreenLogs.Count();
            for (int i = filled; i < 15; i++) r.WriteLine();
            r.WriteLine("===============================================");
            r.WriteLine($" [현재 적 개체 수 : {ctx.Stage.EnemyList.Count} 마리]");
            r.WriteLine("-----------------------------------------------");
            r.WriteLine(" 1. 적 목록 확인 (전체 리스트)");
            r.WriteLine(" 2. 적 추가 (위치/종류 선택)");
            r.WriteLine(" 3. 적 삭제 (ID 지정)");
            r.WriteLine(" 4. 적 레벨 변경 (ID 지정)");
            r.WriteLine(" ESC. 뒤로 가기");
            r.WriteLine("-----------------------------------------------");
            r.Write(" 명령 대기 >> ");
        }

        private static void ShowEnemyListView(GameContext ctx)
        {
            const int pageSize = 10;
            int page = 0;

            while (true)
            {
                ctx.Renderer.Clear();
                int total = ctx.Stage.EnemyList.Count;
                int pages = Math.Max(1, (total + pageSize - 1) / pageSize);
                int start = page * pageSize;
                int end = Math.Min(start + pageSize, total);

                var r = ctx.Renderer;
                r.WriteLine($"--- [현재 스테이지 적 출현 목록] (페이지 {page + 1} / {pages}) ---");
                r.WriteLine(" No | ID |  타입  | Lv | 좌표(X,Y) | 패턴");
                r.WriteLine("------------------------------------------------");

                if (total == 0) r.WriteLine("  (적이 없습니다)");
                else
                {
                    int no = start + 1;
                    for (int i = start; i < end; i++)
                    {
                        var e = ctx.Stage.EnemyList[i];
                        r.WriteLine($"{no++,3} | {e.SpawnId,3} | {e.Name,-4} | {e.Level,2} | ({e.Position.X,3},{e.Position.Y,3}) | {e.Pattern}");
                    }
                }
                r.WriteLine("------------------------------------------------");
                r.WriteLine("[← / A] 이전 페이지   [→ / D] 다음 페이지   [ESC] 로그 화면으로 복귀");
                r.Render();

                // 키 대기 (페이지 네비게이션만은 단순 블로킹 처리)
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape) break;
                else if (key is ConsoleKey.LeftArrow or ConsoleKey.A) { if (page > 0) page--; }
                else if (key is ConsoleKey.RightArrow or ConsoleKey.D) { if (page < pages - 1) page++; }
            }
        }

        private static void InputAddEnemy(GameContext ctx)
        {
            var r = ctx.Renderer; var i = ctx.Input;

            r.WriteLine();
            r.Write(" [추가] 위치 선택 (1:처음 2:끝 3:인덱스) : ");
            string posInput = i.ReadLine();

            int targetIndex = -1; string posLog = "맨 뒤";
            if (posInput == "1") { targetIndex = 0; posLog = "맨 앞"; }
            else if (posInput == "3")
            {
                r.Write(" [추가] 인덱스 입력 : ");
                if (int.TryParse(i.ReadLine(), out int idx)) { targetIndex = idx; posLog = $"{idx}번 위치"; }
                else { targetIndex = 0; posLog = "0번(오류복구)"; }
            }

            r.Write(" [추가] 종류 선택 (1:잡몹 2:중보 3:막보) : ");
            string typeInput = i.ReadLine();
            EnemyType type = typeInput == "2" ? EnemyType.MidBoss : typeInput == "3" ? EnemyType.FinalBoss : EnemyType.Normal;
            string typeLog = type switch { EnemyType.MidBoss => "중보", EnemyType.FinalBoss => "막보", _ => "잡몹" };

            r.Write(" [추가] 생성할 수량 입력 (기본 1) : ");
            int count = 1;
            if (int.TryParse(i.ReadLine(), out int c) && c > 1) count = c;

            for (int n = 0; n < count; n++)
            {
                var e = EnemyFactory.Create(type, order: 0, x: 50, y: 50);
                if (targetIndex == -1) ctx.Stage.AddEnemy(e);
                else ctx.Stage.InsertEnemy(targetIndex + n, e);
            }
            ctx.Log($"[Action] {typeLog} {count}마리 생성 완료 ({posLog})");
        }

        private static void InputDeleteEnemy(GameContext ctx)
        {
            var r = ctx.Renderer; var i = ctx.Input;
            r.WriteLine(); r.Write(" [삭제] 삭제할 적 ID 입력 : ");
            if (int.TryParse(i.ReadLine(), out int id))
            {
                int before = ctx.Stage.EnemyList.Count; ctx.Stage.RemoveEnemyById(id);
                ctx.Log(before > ctx.Stage.EnemyList.Count ? $"[Action] 적 삭제 완료 - Target ID: {id}" : $"[Error] 삭제 실패 - ID {id} 없음");
            }
            else ctx.Log("[Error] 잘못된 입력입니다.");
        }

        private static void InputLevelChange(GameContext ctx)
        {
            var r = ctx.Renderer; var i = ctx.Input;
            r.WriteLine(); r.Write(" [레벨] 변경할 적 ID 입력 : ");
            if (int.TryParse(i.ReadLine(), out int id))
            {
                r.Write($" [레벨] ID {id}에게 적용할 레벨 값 : ");
                if (int.TryParse(i.ReadLine(), out int lv))
                {
                    var target = ctx.Stage.EnemyList.Find(e => e.SpawnId == id);
                    if (target != null) { target.SetLevel(lv); ctx.Log($"[Action] 레벨 변경 - ID:{id} -> Lv.{lv}"); }
                    else ctx.Log($"[Error] ID {id} 없음");
                }
            }
            else ctx.Log("[Error] 잘못된 입력입니다.");
        }
    }
}
