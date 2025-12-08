using LoggerLib;
using ShootingGameLib;

namespace ShootingGameTest
{
    public class LogExportMode
    {
        private GameContext _context;

        public LogExportMode(GameContext context)
        {
            _context = context;
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("===============================================");
            Console.WriteLine("           [ 전체 로그 내보내기 ]");
            Console.WriteLine("===============================================");
            Console.WriteLine(" * 현재까지 기록된 모든 로그를 파일로 저장합니다.");
            Console.WriteLine(" * 파일명을 입력하지 않고 Enter를 누르면 자동 생성됩니다.");
            Console.WriteLine(" * ESC를 누르면 취소하고 메뉴로 돌아갑니다.");
            Console.WriteLine("-----------------------------------------------");

            Console.CursorVisible = true;
            Console.Write(" >> 파일명 입력 (예: mylog) [ESC:취소] : ");

            // [수정] Util의 공용 함수 사용
            string inputName = Util.ReadInputWithCancel();
            Console.CursorVisible = false;

            // 취소(ESC) 시 바로 리턴
            if (inputName == null) return;

            // 저장 로직 실행
            ExportToFile(inputName);

            Console.WriteLine("\n >> 아무 키나 누르면 메뉴로 돌아갑니다.");
            Console.ReadKey(true);
        }

        private void ExportToFile(string fileName)
        {
            try
            {
                // 1. 파일명 가공
                if (string.IsNullOrWhiteSpace(fileName))
                    fileName = $"Export_Log_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                if (!fileName.EndsWith(".txt"))
                    fileName += ".txt";

                // 2. 데이터 복사 (충돌 방지)
                List<string> logsToSave;
                // [중요] FullHistory는 MemoryLogger에 의해 관리되므로 GameContext에서 접근
                lock (_context.LogLock)
                {
                    logsToSave = new List<string>(_context.FullHistory);
                }

                // 3. 파일 쓰기
                if (logsToSave.Count > 0)
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                    File.WriteAllLines(path, logsToSave);

                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine($" [성공] 파일이 저장되었습니다.");
                    Console.WriteLine($" 경로: {fileName}");

                    // 메인 로그에도 기록 (Action 레벨)
                    _context.Logger.WriteLog(LogLevel.Action, $"로그 내보내기 완료: {fileName}");
                }
                else
                {
                    Console.WriteLine("\n [알림] 저장할 로그 데이터가 없습니다.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n [오류] 저장 실패: {ex.Message}");
                // 에러 로그 기록
                _context.Logger.WriteLog(LogLevel.Error, $"로그 내보내기 실패: {ex.Message}");
            }
        }
    }
}