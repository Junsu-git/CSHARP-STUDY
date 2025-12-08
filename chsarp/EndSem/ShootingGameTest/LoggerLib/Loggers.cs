namespace LoggerLib
{
    // 1. 화면 출력용 로거 (최근 N줄만 유지)
    public class ScreenLogger : ILogger
    {
        private List<string> _buffer;
        private int _maxLines;
        private object _lock;

        public ScreenLogger(List<string> buffer, int maxLines, object lockObj)
        {
            _buffer = buffer;
            _maxLines = maxLines;
            _lock = lockObj;
        }

        public void WriteLog(LogLevel level, string message)
        {
            lock (_lock)
            {
                // 화면용 포맷 (시간 + 메시지)
                string log = $"[{DateTime.Now:HH:mm:ss}] [{level}] {message}";
                _buffer.Add(log);
                if (_buffer.Count > _maxLines) _buffer.RemoveAt(0);
            }
        }
    }

    // 2. 백업/히스토리용 로거 (전체 기록 유지)
    public class MemoryLogger : ILogger
    {
        private List<string> _history;
        private object _lock;

        public MemoryLogger(List<string> history, object lockObj)
        {
            _history = history;
            _lock = lockObj;
        }

        public void WriteLog(LogLevel level, string message)
        {
            lock (_lock)
            {
                // 파일 저장용 포맷 (상세 정보)
                string log = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                _history.Add(log);
            }
        }
    }

    // 3. 멀티 로거 (여러 로거에게 동시에 전파)
    public class MultiLogger : ILogger
    {
        private List<ILogger> _loggers;

        public MultiLogger(params ILogger[] loggers)
        {
            _loggers = new List<ILogger>(loggers);
        }

        public void WriteLog(LogLevel level, string message)
        {
            foreach (var logger in _loggers)
            {
                logger.WriteLog(level, message);
            }
        }
    }

    // 4. 파일 로거
    public class FileLogger : ILogger
    {
        private string _filePath;
        private object _lock = new object(); // 파일 동시 접근 방지용

        public FileLogger()
        {
            // 실행 파일 위치에 "Log_날짜.txt" 로 파일 생성
            string fileName = $"Log_{DateTime.Now:yyyyMMdd}.txt";
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        public void WriteLog(LogLevel level, string message)
        {
            // 쓰레드 충돌 방지 (동시에 여러 로그가 들어올 때 깨짐 방지)
            lock (_lock)
            {
                try
                {
                    string logLine = $"[{DateTime.Now:HH:mm:ss}] [{level}] {message}";

                    // 파일 끝에 내용 추가 (파일이 없으면 자동 생성됨)
                    File.AppendAllText(_filePath, logLine + Environment.NewLine);
                }
                catch
                {
                    // 파일 쓰기 실패 시 프로그램이 멈추지 않도록 예외 무시
                    // (예: 파일이 다른 프로그램에 의해 잠겨있을 때)
                }
            }
        }
    }
}