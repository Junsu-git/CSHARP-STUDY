using LoggerLib;
using ShootingGameLib;
using System;

namespace ShootingGameTest
{
    public class PlayerTestMode
    {
        private GameContext _context;

        public PlayerTestMode(GameContext context)
        {
            _context = context;
        }

        public void Run()
        {
            lock (_context.LogLock) _context.ScreenBuffer.Clear();
            _context.Logger.WriteLog(LogLevel.Info, "플레이어 테스트 화면 진입");

            while (true)
            {
                DrawScreen();

                // 입력 대기 (반응성을 위해 루프 사용하지만, 메뉴 진입 시엔 버퍼 정리)
                while (Console.KeyAvailable) Console.ReadKey(true);

                // 여기서는 즉시 반응을 위해 if문 대신 ReadKey 사용 (단, 루프는 아님)
                // 대신 조작감이 중요하므로 버퍼 비우기를 최소화

                var keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape) break;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1: // 1. 플레이어 추가
                    case ConsoleKey.NumPad1:
                        Util.ClearKeyBuffer(); // 메뉴 진입 전 버퍼 정리
                        if (_context.Players.Count < 2)
                        {
                            _context.Players.Add(new Player("Player2", PlayerType.P2));
                            _context.Logger.WriteLog(LogLevel.Action, "Player2 추가됨 (조작: IJKL / ; / ')");
                        }
                        else
                            _context.Logger.WriteLog(LogLevel.Warning, "이미 2인 플레이 중입니다.");
                        break;

                    case ConsoleKey.D2: // 2. 레벨 변경
                    case ConsoleKey.NumPad2:
                        Util.ClearKeyBuffer();
                        HandleLevelChange();
                        break;

                    default: // 이동 조작
                        foreach (var p in _context.Players)
                        {
                            string log = p.HandleInput(keyInfo.Key);
                            if (log != null)
                                _context.Logger.WriteLog(LogLevel.Action, log);
                        }
                        break;
                }
            }
        }

        private void DrawScreen()
        {
            Console.Clear();
            Console.WriteLine("===============================================");
            Console.WriteLine("             [ 플레이어 동작 로그 ]");
            Console.WriteLine("===============================================");

            lock (_context.LogLock)
            {
                for (int i = 0; i < GameContext.MaxScreenLogLines; i++)
                {
                    if (i < _context.ScreenBuffer.Count)
                        Console.WriteLine($" {_context.ScreenBuffer[i]}");
                    else
                        Console.WriteLine();
                }
            }

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine(" [현재 상태]");
            foreach (var p in _context.Players)
                Console.WriteLine($" ▶ {p.Name,-7} : HP {p.CurrentHp,3} / ATK {p.CurrentAttack,2} / Pos ({p.X,2}, {p.Y,2})");
            Console.WriteLine(" [WASD] 1P 조작   [IJKL] 2P 조작");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine(" 1. 플레이어 추가 (2P)");
            Console.WriteLine(" 2. 플레이어 레벨 변경");
            Console.WriteLine(" ESC. 뒤로 가기");
            Console.WriteLine("===============================================");
            Console.Write(" 명령 대기 >> ");
        }

        private void HandleLevelChange()
        {
            Console.WriteLine();
            Console.CursorVisible = true;

            // 1. 플레이어가 1명일 경우 (2P 없음)
            if (_context.Players.Count < 2)
            {
                Console.Write(" [레벨] 변경할 레벨 값 입력 [ESC:취소] : ");
                string input = Util.ReadInputWithCancel(); // [수정] Util 함수 사용
                if (input == null) { Console.CursorVisible = false; return; }

                if (int.TryParse(input, out int level))
                {
                    _context.Players[0].SetLevel(level);
                    _context.Logger.WriteLog(LogLevel.Action, $"Player1 레벨 {level}(으)로 변경 완료.");
                }
                else
                    _context.Logger.WriteLog(LogLevel.Error, "잘못된 숫자 입력입니다.");
            }
            // 2. 플레이어가 2명 이상일 경우 (2P 존재)
            else
            {
                Console.Write(" [레벨] 변경 방식 선택 (1:단일 / 2:전체) [ESC:취소] : ");
                var modeKey = Console.ReadKey();
                if (modeKey.Key == ConsoleKey.Escape) { Console.CursorVisible = false; return; }
                Console.WriteLine();

                if (modeKey.Key == ConsoleKey.D1 || modeKey.Key == ConsoleKey.NumPad1)
                {
                    // 단일 변경
                    Console.Write("   >> 대상 선택 (1:Player1 / 2:Player2) : ");
                    var targetKey = Console.ReadKey();
                    if (targetKey.Key == ConsoleKey.Escape) { Console.CursorVisible = false; return; }
                    Console.WriteLine();

                    int targetIdx = -1;
                    if (targetKey.Key == ConsoleKey.D1 || targetKey.Key == ConsoleKey.NumPad1) targetIdx = 0;
                    else if (targetKey.Key == ConsoleKey.D2 || targetKey.Key == ConsoleKey.NumPad2) targetIdx = 1;

                    if (targetIdx != -1)
                    {
                        Console.Write($"   >> [{_context.Players[targetIdx].Name}] 적용할 레벨 : ");
                        string levelInput = Util.ReadInputWithCancel(); // [수정] Util 함수 사용
                        if (levelInput == null) { Console.CursorVisible = false; return; }

                        if (int.TryParse(levelInput, out int level))
                        {
                            _context.Players[targetIdx].SetLevel(level);
                            _context.Logger.WriteLog(LogLevel.Action, $"{_context.Players[targetIdx].Name} 레벨 {level}(으)로 변경.");
                        }
                    }
                    else
                        _context.Logger.WriteLog(LogLevel.Error, "잘못된 대상 선택입니다.");
                }
                else if (modeKey.Key == ConsoleKey.D2 || modeKey.Key == ConsoleKey.NumPad2)
                {
                    // 전체 변경
                    Console.Write("   >> 전체 적용할 레벨 : ");
                    string levelInput = Util.ReadInputWithCancel(); // [수정] Util 함수 사용
                    if (levelInput == null) { Console.CursorVisible = false; return; }

                    if (int.TryParse(levelInput, out int level))
                    {
                        foreach (var p in _context.Players) p.SetLevel(level);
                        _context.Logger.WriteLog(LogLevel.Action, $"모든 플레이어 레벨 {level}(으)로 변경.");
                    }
                }
                else
                    _context.Logger.WriteLog(LogLevel.Error, "잘못된 모드 선택입니다.");
            }
            Console.CursorVisible = false;
        }
    }
}