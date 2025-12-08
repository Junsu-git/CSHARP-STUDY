using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ==========================================================
// [Project 1] ShootingGameLib : 게임 로직 라이브러리
// ==========================================================
namespace ShootingGameLib
{
    // 공통 객체 기본 설계 (요구사항 8)
    public abstract class Unit
    {
        public string Name { get; protected set; }
        public int Level { get; protected set; } = 1;

        // 기본 스탯 + 레벨 스탯 + 보너스 스탯
        protected int BaseHP;
        protected int BaseAttack;
        public int BonusHP { get; set; } = 0;
        public int BonusAttack { get; set; } = 0;

        // 계산된 최종 스탯
        public int MaxHP => BaseHP + (Level * 10) + BonusHP;
        public int AttackPower => BaseAttack + (Level * 2) + BonusAttack;

        public Unit(string name, int hp, int atk)
        {
            Name = name;
            BaseHP = hp;
            BaseAttack = atk;
        }

        public void SetLevel(int newLevel)
        {
            if (newLevel < 1) newLevel = 1;
            Level = newLevel;
        }

        public abstract string Action(); // 다형성: 테스트 시 출력할 문구
    }

    public class Player : Unit
    {
        public int PlayerIndex { get; private set; } // 1P, 2P 구분

        public Player(int index) : base($"Player {index}P", 100, 10)
        {
            PlayerIndex = index;
        }

        public override string Action() => "대기 중...";

        // 입력에 따른 반응 동작 (요구사항 9-a)
        public string ReactToInput(ConsoleKey key)
        {
            string act = "";
            // 1P 조작
            if (PlayerIndex == 1)
            {
                if (key == ConsoleKey.W) act = "위로 이동";
                else if (key == ConsoleKey.S) act = "아래로 이동";
                else if (key == ConsoleKey.A) act = "왼쪽으로 이동";
                else if (key == ConsoleKey.D) act = "오른쪽으로 이동";
                else if (key == ConsoleKey.Spacebar) act = "기본 미사일 발사!";
                else if (key == ConsoleKey.Enter) act = "필살기 사용!";
            }
            // 2P 조작
            else if (PlayerIndex == 2)
            {
                if (key == ConsoleKey.I) act = "위로 이동";
                else if (key == ConsoleKey.K) act = "아래로 이동";
                else if (key == ConsoleKey.J) act = "왼쪽으로 이동";
                else if (key == ConsoleKey.L) act = "오른쪽으로 이동";
                else if (key == ConsoleKey.Oem1) act = "기본 미사일 발사!"; // ';' 키
                else if (key == ConsoleKey.Oem7) act = "필살기 사용!"; // ''' 키
            }

            if (string.IsNullOrEmpty(act)) return null;
            return $"[{Name}] {act} (ATK: {AttackPower})";
        }
    }

    public enum EnemyType { Normal, MidBoss, Boss }

    public class Enemy : Unit
    {
        public int ID { get; private set; }
        public EnemyType Type { get; private set; }
        public int SpawnX { get; set; }
        public int SpawnY { get; set; }
        public int SpawnOrder { get; set; } // 출현 순서
        public string Pattern { get; set; }

        // 생성자는 Factory에서만 접근하도록 설계하는 것이 정석이나, 
        // 과제 구현상 public으로 두되 Factory 사용을 강제함.
        public Enemy(int id, EnemyType type, int level, int order, int x, int y)
            : base(type.ToString(), 0, 0)
        {
            ID = id;
            Type = type;
            SpawnOrder = order;
            SpawnX = x; SpawnY = y;
            SetLevel(level);

            // 타입별 기본 스탯 설정
            switch (type)
            {
                case EnemyType.Normal: BaseHP = 30; BaseAttack = 5; Pattern = "직진"; break;
                case EnemyType.MidBoss: BaseHP = 150; BaseAttack = 15; Pattern = "지그재그"; break;
                case EnemyType.Boss: BaseHP = 500; BaseAttack = 40; Pattern = "유도탄 발사"; break;
            }
        }

        public override string Action() => $"[{Name}] 패턴: {Pattern} 실행 중 (Pos: {SpawnX},{SpawnY})";
    }

    // [핵심 요구사항 6] 적 객체 생성 공장 (Factory Pattern)
    public static class EnemyFactory
    {
        private static int idCounter = 1;

        public static Enemy CreateEnemy(EnemyType type, int level, int order, int x, int y)
        {
            // 객체 생성 로직을 캡슐화
            return new Enemy(idCounter++, type, level, order, x, y);
        }
    }

    // 게임 전체 데이터를 관리하는 클래스
    public class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance => instance ??= new GameManager();

        public List<Player> Players { get; private set; } = new List<Player>();
        public List<Enemy> Enemies { get; private set; } = new List<Enemy>();

        public void Initialize()
        {
            Players.Clear();
            Enemies.Clear();

            // 요구사항 3: 기본 플레이어 1명 생성
            Players.Add(new Player(1));

            // 요구사항 2,4,5: 스테이지에 맞춰 적 생성 (자동 생성 시뮬레이션)
            // 일반 적 3마리, 중간보스 1마리, 보스 1마리 예시
            AddEnemy(EnemyType.Normal, 1, 1, 10, 50);
            AddEnemy(EnemyType.Normal, 2, 2, 30, 20);
            AddEnemy(EnemyType.Normal, 1, 3, 50, 80);
            AddEnemy(EnemyType.MidBoss, 5, 4, 40, 50);
            AddEnemy(EnemyType.Boss, 10, 5, 40, 10);
        }

        // 리스트 관리 헬퍼
        public void AddEnemy(EnemyType type, int lv, int order, int x, int y)
        {
            Enemy e = EnemyFactory.CreateEnemy(type, lv, order, x, y);
            Enemies.Add(e);
            // 출현 순서대로 정렬
            Enemies = Enemies.OrderBy(en => en.SpawnOrder).ToList();
        }

        public void RemoveEnemy(int id)
        {
            var target = Enemies.FirstOrDefault(e => e.ID == id);
            if (target != null) Enemies.Remove(target);
        }

        public void AddPlayer2()
        {
            if (Players.Count < 2)
                Players.Add(new Player(2));
        }
    }
}

// ==========================================================
// [Project 2] GameTestConsole : 테스트 실행 프로그램 (UI)
// ==========================================================
namespace GameTestConsole
{
    using ShootingGameLib;

    class Program
    {
        static void Main(string[] args)
        {
            // 콘솔 설정
            Console.CursorVisible = false;
            if (OperatingSystem.IsWindows()) Console.SetWindowSize(100, 30);

            // 데이터 초기화
            GameManager.Instance.Initialize();

            // 씬 매니저 시작
            SceneManager.LoadScene(new MainMenuScene());
            SceneManager.Run();
        }
    }

    // --- Scene System ---
    static class SceneManager
    {
        private static Scene currentScene;
        public static void LoadScene(Scene scene) { currentScene = scene; currentScene.Enter(); }
        public static void Run()
        {
            while (true)
            {
                currentScene.Draw();
                if (Console.KeyAvailable)
                {
                    currentScene.HandleInput(Console.ReadKey(true));
                }
            }
        }
    }

    abstract class Scene
    {
        public abstract void Enter();
        public abstract void Draw();
        public abstract void HandleInput(ConsoleKeyInfo key);
    }

    // --- 메인 메뉴 씬 ---
    class MainMenuScene : Scene
    {
        string[] menus = { "1. 플레이어 기체 테스트", "2. 적 목록 관리/테스트", "3. 객체 레벨 변경", "4. 종료" };
        int selectIndex = 0;

        public override void Enter() { Console.Clear(); }
        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("=====================================================");
            Console.WriteLine("     슈팅 게임 개발 테스트 모니터링 (Ver 0.5)        ");
            Console.WriteLine("=====================================================");

            for (int i = 0; i < menus.Length; i++)
            {
                if (i == selectIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($" >> {menus[i]}".PadRight(50));
                    Console.ResetColor();
                }
                else Console.WriteLine($"    {menus[i]}".PadRight(50));
            }
            Console.WriteLine("=====================================================");
            Console.WriteLine(" [UP/DOWN] 이동, [ENTER] 선택");
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow: selectIndex = Math.Max(0, selectIndex - 1); break;
                case ConsoleKey.DownArrow: selectIndex = Math.Min(menus.Length - 1, selectIndex + 1); break;
                case ConsoleKey.Enter:
                    if (selectIndex == 0) SceneManager.LoadScene(new PlayerTestScene());
                    else if (selectIndex == 1) SceneManager.LoadScene(new EnemyListScene());
                    else if (selectIndex == 2) SceneManager.LoadScene(new LevelEditScene());
                    else Environment.Exit(0);
                    break;
            }
        }
    }

    // --- 9-a. 플레이어 기체 테스트 씬 ---
    class PlayerTestScene : Scene
    {
        List<string> logs = new List<string>();

        public override void Enter() { Console.Clear(); logs.Add("[안내] 1P: WASD/Space/Enter, 2P: IJKL/;/'"); }
        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("[ 플레이어 테스트 모드 ] (ESC: 뒤로가기, F1: 2P 추가)".PadRight(80));
            Console.WriteLine("-----------------------------------------------------");

            // 플레이어 상태 표시
            foreach (var p in GameManager.Instance.Players)
            {
                Console.WriteLine($" {p.Name} | HP:{p.MaxHP} | ATK:{p.AttackPower} | Lv:{p.Level}".PadRight(80));
            }
            Console.WriteLine("-----------------------------------------------------");

            // 로그 출력
            for (int i = 0; i < 15; i++)
            {
                if (i < logs.Count) Console.WriteLine(logs[logs.Count - 1 - i].PadRight(80)); // 최신순 출력
                else Console.WriteLine("".PadRight(80));
            }
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Escape) SceneManager.LoadScene(new MainMenuScene());
            else if (key.Key == ConsoleKey.F1)
            {
                GameManager.Instance.AddPlayer2();
                logs.Add("[System] 2P 플레이어가 참전했습니다!");
            }
            else
            {
                foreach (var p in GameManager.Instance.Players)
                {
                    string log = p.ReactToInput(key.Key);
                    if (log != null) logs.Add(log);
                }
            }
        }
    }

    // --- 9-b. 적 목록 테스트 씬 ---
    class EnemyListScene : Scene
    {
        // 0: 리스트확인, 1: 삭제모드, 2: 추가모드
        int mode = 0;

        public override void Enter() { Console.Clear(); }
        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"[ 적 생성 관리자 ] 모드: {(mode == 0 ? "조회" : (mode == 1 ? "삭제(ID입력)" : "추가(자동)"))} (ESC:뒤로, D:삭제, A:추가)".PadRight(90));
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine(" ID | Rank    | Lv | Order | Pos(X,Y) | Pattern        | HP  | ATK");
            Console.WriteLine("-----------------------------------------------------------------------------");

            var enemies = GameManager.Instance.Enemies;
            for (int i = 0; i < 15; i++)
            {
                if (i < enemies.Count)
                {
                    var e = enemies[i];
                    string info = $" {e.ID,-2} | {e.Type,-7} | {e.Level,-2} | {e.SpawnOrder,-5} | ({e.SpawnX},{e.SpawnY})".PadRight(43) + $" | {e.Pattern,-14} | {e.MaxHP,-3} | {e.AttackPower,-3}";
                    Console.WriteLine(info.PadRight(90));
                }
                else Console.WriteLine("".PadRight(90));
            }
            Console.WriteLine("-----------------------------------------------------------------------------");

            if (mode == 1) Console.Write(" >> 삭제할 ID 입력 후 엔터: ".PadRight(50));
            else if (mode == 2) Console.Write(" >> [N]일반 [M]중간보스 [B]보스 추가 (자동삽입): ".PadRight(50));
            else Console.WriteLine("".PadRight(50));
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (mode == 0)
            {
                if (key.Key == ConsoleKey.Escape) SceneManager.LoadScene(new MainMenuScene());
                else if (key.Key == ConsoleKey.D) { mode = 1; Console.Clear(); }
                else if (key.Key == ConsoleKey.A) { mode = 2; Console.Clear(); }
            }
            else if (mode == 1) // 삭제 입력
            {
                if (char.IsDigit(key.KeyChar))
                {
                    Console.Write(key.KeyChar);
                    string idStr = key.KeyChar.ToString() + Console.ReadLine();
                    if (int.TryParse(idStr, out int id)) GameManager.Instance.RemoveEnemy(id);
                    mode = 0; Console.Clear();
                }
                else { mode = 0; }
            }
            else if (mode == 2) // 추가 입력
            {
                EnemyType t = EnemyType.Normal;
                if (key.Key == ConsoleKey.M) t = EnemyType.MidBoss;
                if (key.Key == ConsoleKey.B) t = EnemyType.Boss;

                // 임의의 위치에 삽입 테스트 (랜덤성)
                Random r = new Random();
                GameManager.Instance.AddEnemy(t, r.Next(1, 10), r.Next(1, 10), r.Next(0, 100), r.Next(0, 30));
                mode = 0; Console.Clear();
            }
        }
    }

    // --- 9-c. 레벨 변경 테스트 씬 ---
    class LevelEditScene : Scene
    {
        string msg = "";
        public override void Enter() { Console.Clear(); }
        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("[ 레벨 데이터 조작 ] (ESC: 뒤로가기)".PadRight(80));
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine(" 1. 플레이어 전체 공격력 레벨 +1");
            Console.WriteLine(" 2. 적 전체 공격력 레벨 +1");
            Console.WriteLine(" 3. 초기화 (Lv 1)");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine($" 결과: {msg}".PadRight(80));
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Escape) SceneManager.LoadScene(new MainMenuScene());
            else if (key.Key == ConsoleKey.D1)
            {
                foreach (var p in GameManager.Instance.Players) p.SetLevel(p.Level + 1);
                msg = "플레이어 레벨이 증가했습니다.";
            }
            else if (key.Key == ConsoleKey.D2)
            {
                foreach (var e in GameManager.Instance.Enemies) e.SetLevel(e.Level + 1);
                msg = "적들의 레벨이 증가했습니다.";
            }
            else if (key.Key == ConsoleKey.D3)
            {
                foreach (var p in GameManager.Instance.Players) p.SetLevel(1);
                foreach (var e in GameManager.Instance.Enemies) e.SetLevel(1);
                msg = "모든 레벨이 초기화되었습니다.";
            }
        }
    }
}