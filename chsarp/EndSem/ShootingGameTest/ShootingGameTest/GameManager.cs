using LoggerLib;
using ShootingGameLib;

namespace ShootingGameTest
{
    public class GameManager
    {
        // 게임 데이터 컨텍스트
        private GameContext _context;

        // UI 상수
        private const int UIWidth = 47;
        private static readonly string LineDouble = new string('=', UIWidth);
        private static readonly string LineSingle = new string('-', UIWidth);

        public GameManager()
        {
            _context = new GameContext();
        }

        // [핵심] 게임의 시작점
        public void Run()
        {
            Initialize();

            // 메인 루프
            while (true)
            {
                DrawMainMenu();
                Util.ClearKeyBuffer();

                var key = Console.ReadKey(true);

                // 모드 전환 및 기능 수행
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        new PlayerTestMode(_context).Run();
                        break;

                    case ConsoleKey.D2:
                        new EnemyTestMode(_context).Run();
                        break;
                    case ConsoleKey.D3:
                        new LogExportMode(_context).Run();
                        break;

                    case ConsoleKey.LeftArrow:
                        ChangeStage(_context.CurrentStage - 1);
                        break;

                    case ConsoleKey.RightArrow:
                        ChangeStage(_context.CurrentStage + 1);
                        break;

                    case ConsoleKey.Escape:
                        return; // 프로그램 종료
                }
            }
        }

        private void Initialize()
        {
            // 초기 로그
            _context.Logger.WriteLog(LogLevel.Info, "게임 매니저 초기화 완료.");

            // 기본 데이터 생성
            _context.Players.Add(new Player("Player1", PlayerType.P1));

            // 1스테이지 로드
            LoadStage(1);
        }

        private void DrawMainMenu()
        {
            Console.Clear();
            Console.WriteLine(LineDouble);
            Console.WriteLine("           [ 슈팅 게임 테스트 모니터 ]");
            Console.WriteLine(LineDouble);
            Console.WriteLine($" <  STAGE  {_context.CurrentStage}  >\n");
            Console.WriteLine(" 1. 플레이어 기체 테스트");
            Console.WriteLine(" 2. 적 객체 동작 테스트");
            Console.WriteLine(" 3. 전체 로그 내보내기 (파일 저장)");
            Console.WriteLine(LineSingle);
            Console.WriteLine(" [←] 이전 스테이지    [→] 다음 스테이지");
            Console.WriteLine(" ESC. 프로그램 종료");
            Console.WriteLine(LineDouble);
            Console.Write(" >> 명령 대기");
        }

        // --- 스테이지 관리 ---

        private void ChangeStage(int targetStage)
        {
            if (targetStage < 1) targetStage = 1;
            if (targetStage > 3) targetStage = 3;

            if (targetStage != _context.CurrentStage)
            {
                LoadStage(targetStage);
                _context.Logger.WriteLog(LogLevel.Info, $"스테이지 {targetStage} 로드 완료.");
            }
        }

        private void LoadStage(int stage)
        {
            _context.CurrentStage = stage;
            _context.StageMng.ClearEnemies();
            EnemyFactory.ResetIdCounter();

            switch (stage)
            {
                case 1:
                    for (int i = 0; i < 20; i++)
                    {
                        int order = i + 1;
                        EnemyType type = EnemyType.Normal;
                        int x = (i % 5) * 50 + 50;

                        if (order == 20) type = EnemyType.FinalBoss;
                        else if (order % 5 == 0) type = EnemyType.MidBoss;

                        _context.StageMng.AddEnemy(EnemyFactory.CreateEnemy(type, order, x, 100));
                    }
                    break;

                case 2:
                    for (int i = 0; i < 30; i++)
                    {
                        int x = (i % 10) * 10 + 10;
                        _context.StageMng.AddEnemy(EnemyFactory.CreateEnemy(EnemyType.Normal, i + 1, x, 50));
                    }
                    _context.StageMng.AddEnemy(EnemyFactory.CreateEnemy(EnemyType.MidBoss, 31, 30, 80));
                    _context.StageMng.AddEnemy(EnemyFactory.CreateEnemy(EnemyType.MidBoss, 32, 70, 80));
                    break;

                case 3:
                    _context.StageMng.AddEnemy(EnemyFactory.CreateEnemy(EnemyType.MidBoss, 1, 20, 100));
                    _context.StageMng.AddEnemy(EnemyFactory.CreateEnemy(EnemyType.MidBoss, 2, 80, 100));
                    _context.StageMng.AddEnemy(EnemyFactory.CreateEnemy(EnemyType.FinalBoss, 3, 50, 50));
                    break;

                default:
                    _context.Logger.WriteLog(LogLevel.Warning, $"정의되지 않은 Stage {stage}");
                    break;
            }

            _context.Logger.WriteLog(LogLevel.Info, $"Stage {stage} 데이터 로드 완료 ({_context.StageMng.EnemyList.Count}마리)");
        }

        //private void StartAutoSaveTask()
        //{
        //    Task.Run(async () =>
        //    {
        //        while (true)
        //        {
        //            await Task.Delay(60000); // 1분
        //            try
        //            {
        //                string file = $"Backup_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        //                List<string> logs;
        //                lock (_context.LogLock) logs = new List<string>(_context.FullHistory);
        //                if (logs.Count > 0) File.WriteAllLines(file, logs);
        //            }
        //            catch { }
        //        }
        //    });
        //}
    }
}