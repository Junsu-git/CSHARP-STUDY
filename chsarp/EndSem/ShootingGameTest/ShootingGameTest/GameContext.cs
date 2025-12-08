using LoggerLib;
using ShootingGameLib;

namespace ShootingGameTest
{
    public class GameContext
    {
        public List<Player> Players { get; set; } = new List<Player>();
        public StageManager StageMng { get; set; } = new StageManager();

        // [수정] 직접 리스트를 관리하던 방식 -> 로거에게 위임
        public ILogger Logger { get; set; }
        public ILogger FLogger { get; set; }

        // UI에서 그릴 때 필요한 데이터 원본 (ScreenLogger와 공유됨)
        public List<string> ScreenBuffer { get; set; } = new List<string>();
        // 파일 저장 쓰레드가 읽을 데이터 원본 (MemoryLogger와 공유됨)
        public List<string> FullHistory { get; set; } = new List<string>();

        public object LogLock { get; set; } = new object();
        public int CurrentStage { get; set; } = 1;
        public const int MaxScreenLogLines = 15;

        // 생성자에서 로거 조립
        public GameContext()
        {
            // 화면용 로거
            var screenLogger = new ScreenLogger(ScreenBuffer, MaxScreenLogLines, LogLock);
            // 히스토리용 로거
            var memoryLogger = new MemoryLogger(FullHistory, LogLock);

            // 파일용 로거
            FLogger = new FileLogger();

            // 화면 + 메모리 로거
            Logger = new MultiLogger(screenLogger, memoryLogger);
        }
    }
}