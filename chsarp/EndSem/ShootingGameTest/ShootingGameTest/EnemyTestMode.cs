using LoggerLib;
using ShootingGameLib;
using System;
using System.Collections.Generic;

namespace ShootingGameTest
{
    public class EnemyTestMode
    {
        private GameContext _context;

        public EnemyTestMode(GameContext context)
        {
            _context = context;
        }

        public void Run()
        {
            lock (_context.LogLock) _context.ScreenBuffer.Clear();
            _context.Logger.WriteLog(LogLevel.Info, $"적 테스팅 모드 진입 (Stage {_context.CurrentStage})");

            while (true)
            {
                DrawScreen();
                Util.ClearKeyBuffer();

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape) return;

                switch (key.Key)
                {
                    case ConsoleKey.D1: ShowList(); break;
                    case ConsoleKey.D2: AddEnemy(); break;
                    case ConsoleKey.D3: DeleteEnemy(); break;
                    case ConsoleKey.D4: ChangeLevel(); break;
                }
            }
        }

        private void DrawScreen()
        {
            Console.Clear();
            Console.WriteLine("===============================================");
            Console.WriteLine("             [ 적 객체 동작 로그 ]");
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
            Console.WriteLine($" [현재 적 개체 수 : {_context.StageMng.EnemyList.Count} 마리]");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine(" 1. 적 목록 확인");
            Console.WriteLine(" 2. 적 추가");
            Console.WriteLine(" 3. 적 삭제");
            Console.WriteLine(" 4. 적 레벨 변경");
            Console.WriteLine(" ESC. 뒤로 가기");
            Console.WriteLine("===============================================");
            Console.Write(" 명령 대기 >> ");
        }

        // =========================================================
        //  세부 기능 구현
        // =========================================================

        private void ShowList()
        {
            int pageSize = GameContext.MaxScreenLogLines;
            int page = 0;
            var enemyList = _context.StageMng.EnemyList;

            while (true)
            {
                Console.Clear();

                int totalEnemies = enemyList.Count;
                int totalPages = (totalEnemies + pageSize - 1) / pageSize;
                if (totalPages == 0) totalPages = 1;

                int startIndex = page * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, totalEnemies);

                Console.WriteLine("===============================================");
                Console.WriteLine($"      [ 적 목록 전체 조회 ] ({page + 1}/{totalPages})");
                Console.WriteLine("===============================================");
                Console.WriteLine("  No  |  ID  |   타입   |  Lv  |   HP   |  ATK  | 패턴");
                Console.WriteLine("-----------------------------------------------");

                if (totalEnemies == 0)
                {
                    Console.WriteLine("  (적이 없습니다)");
                }
                else
                {
                    int no = startIndex + 1;
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        var e = enemyList[i];
                        Console.WriteLine($"{no,4}  | {e.SpawnId,4} |   {e.Name}   | {e.Level,4} | {e.CurrentHp,6} | {e.CurrentAttack,5} | {e.Pattern}");
                    }
                }

                int printed = (totalEnemies == 0) ? 1 : (endIndex - startIndex);
                for (int i = 0; i < pageSize - printed; i++) Console.WriteLine();

                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine(" [←/A] 이전   [→/D] 다음   [ESC] 뒤로가기");
                Console.WriteLine("===============================================");

                Util.ClearKeyBuffer();
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape) break;

                if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                {
                    if (page > 0) page--;
                }
                else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                {
                    if (page < totalPages - 1) page++;
                }
            }
        }

        private void AddEnemy()
        {
            int inputY = Console.CursorTop;
            Console.CursorVisible = true;

            // 1. 위치
            Util.ClearLine(inputY);
            Console.Write(" [추가] 위치 (1:처음 2:끝 3:인덱스) [ESC:취소] : ");
            string posIn = Util.ReadInputWithCancel(); // [수정] Util 함수 사용
            if (posIn == null) { Console.CursorVisible = false; return; }

            int targetIdx = -1;
            string posLog = "맨 뒤";
            if (posIn == "1") { targetIdx = 0; posLog = "맨 앞"; }
            else if (posIn == "3")
            {
                Util.ClearLine(inputY);
                Console.Write(" [추가] 인덱스 입력 [ESC:취소] : ");
                string idxIn = Util.ReadInputWithCancel(); // [수정] Util 함수 사용
                if (idxIn == null) { Console.CursorVisible = false; return; }
                int.TryParse(idxIn, out targetIdx);
                posLog = $"{targetIdx}번";
            }

            // 2. 종류
            Util.ClearLine(inputY);
            Console.Write(" [추가] 종류 (1:잡몹 2:중보 3:막보) [ESC:취소] : ");
            string typeIn = Util.ReadInputWithCancel(); // [수정] Util 함수 사용
            if (typeIn == null) { Console.CursorVisible = false; return; }

            EnemyType type = EnemyType.Normal;
            string typeLog = "잡몹";
            if (typeIn == "2") { type = EnemyType.MidBoss; typeLog = "중보"; }
            else if (typeIn == "3") { type = EnemyType.FinalBoss; typeLog = "막보"; }

            // 3. 수량
            Util.ClearLine(inputY);
            Console.Write(" [추가] 수량 (기본 1) [ESC:취소] : ");
            string countIn = Util.ReadInputWithCancel(); // [수정] Util 함수 사용
            if (countIn == null) { Console.CursorVisible = false; return; }

            int count = 1;
            if (int.TryParse(countIn, out int c) && c > 1) count = c;

            Console.CursorVisible = false;

            for (int i = 0; i < count; i++)
            {
                var enemy = EnemyFactory.CreateEnemy(type, 0, 50, 50);
                if (targetIdx == -1) _context.StageMng.AddEnemy(enemy);
                else _context.StageMng.InsertEnemy(targetIdx + i, enemy);
            }

            // [로그] Action
            _context.Logger.WriteLog(LogLevel.Action, $"적 생성 완료 - {typeLog} {count}마리 ({posLog})");
        }

        private void DeleteEnemy()
        {
            int y = Console.CursorTop;
            Util.ClearLine(y);
            Console.CursorVisible = true;

            Console.Write(" [삭제] ID 입력 [ESC:취소] : ");
            string input = Util.ReadInputWithCancel(); // [수정] Util 함수 사용
            if (input == null) { Console.CursorVisible = false; return; }

            if (int.TryParse(input, out int id))
            {
                int before = _context.StageMng.EnemyList.Count;
                _context.StageMng.RemoveEnemyById(id);
                if (before > _context.StageMng.EnemyList.Count)
                    // [로그] Action
                    _context.Logger.WriteLog(LogLevel.Action, $"ID {id} 삭제 완료");
                else
                    // [로그] Error
                    _context.Logger.WriteLog(LogLevel.Error, $"ID {id} 삭제 실패 (존재하지 않음)");
            }
            Console.CursorVisible = false;
        }

        private void ChangeLevel()
        {
            int y = Console.CursorTop;
            Util.ClearLine(y);
            Console.CursorVisible = true;

            Console.Write(" [레벨] ID 입력 [ESC:취소] : ");
            string idIn = Util.ReadInputWithCancel(); // [수정] Util 함수 사용
            if (idIn == null) { Console.CursorVisible = false; return; }

            if (int.TryParse(idIn, out int id))
            {
                Util.ClearLine(y);
                Console.Write($" [레벨] ID {id}의 새 레벨 [ESC:취소] : ");
                string lvIn = Util.ReadInputWithCancel(); // [수정] Util 함수 사용
                if (lvIn == null) { Console.CursorVisible = false; return; }

                if (int.TryParse(lvIn, out int lv))
                {
                    var target = _context.StageMng.EnemyList.Find(e => e.SpawnId == id);
                    if (target != null)
                    {
                        target.SetLevel(lv);
                        // [로그] Action
                        _context.Logger.WriteLog(LogLevel.Action, $"ID {id} -> Lv.{lv} 변경");
                    }
                    else
                        // [로그] Error
                        _context.Logger.WriteLog(LogLevel.Error, $"ID {id} 없음");
                }
            }
            Console.CursorVisible = false;
        }
    }
}