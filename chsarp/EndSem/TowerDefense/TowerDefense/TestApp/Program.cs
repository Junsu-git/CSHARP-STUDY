using System;
using System.Linq;
using TowerDefense.Core;

namespace TowerDefense.TestApp
{
    class Program
    {
        static GameManager gameManager;

        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("--- 타워 디펜스 객체 TEST 모니터링 프로그램 시작 (Stage 1) ---");
            Console.WriteLine("---------------------------------------------------------");

            // 요구사항 2: 프로그램 시작 시 객체 생성 및 준비
            gameManager = new GameManager();

            bool isRunning = true;
            while (isRunning)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine().ToLower();
                Console.WriteLine();

                switch (choice)
                {
                    case "a": TowerTestMenu(); break;
                    case "b": EnemyTestMenu(); break;
                    case "c": LevelChangeMenu(); break;
                    case "x": isRunning = false; break;
                    default: Console.WriteLine(">> [오류] 잘못된 메뉴 선택입니다. 다시 시도해 주세요."); break;
                }
                Console.WriteLine("\n---------------------------------------------------------");
            }

            Console.WriteLine("--- TEST 프로그램 종료 ---");
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("\n## 메인 테스트 메뉴 (준비된 기능 테스트)");
            Console.WriteLine("a. 플레이어 타워 테스트");
            Console.WriteLine("b. 적 목록 테스트");
            Console.WriteLine("c. 객체 Level 변경 (공격력)");
            Console.WriteLine("x. 종료");
            Console.Write(">> 메뉴를 선택하세요: ");
        }

        // 요구사항 9.a: 플레이어 타워 테스트
        static void TowerTestMenu()
        {
            Console.WriteLine("\n### 1. 플레이어 타워 테스트");
            var (count, list) = gameManager.GetTowerListInfo();
            Console.WriteLine($"[결과] 총 타워 갯수: {count}개");
            Console.WriteLine("--- 타워 목록 (ID 앞 8자리, 현재 능력치) ---");
            Console.WriteLine(list);
            Console.WriteLine("---------------------------------------------\n");

            Console.WriteLine("a1. 타워 상세 정보 확인 (ID/번호)");
            Console.WriteLine("a2. 타워 삭제 (ID/번호)");
            Console.WriteLine("a3. 타워 타입 변경 (ID/번호)");
            Console.WriteLine("a4. 타워 추가 (Type)");
            Console.Write(">> 세부 메뉴를 선택하세요 (또는 Enter로 돌아가기): ");
            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "a1": CheckTowerDetail(); break;
                case "a2": RemoveTower(); break;
                case "a3": ChangeTowerType(); break;
                case "a4": AddTower(); break;
            }
        }

        static void CheckTowerDetail()
        {
            Console.Write("확인할 타워의 ID(앞 8자리) 또는 번호(목록)를 입력하세요: ");
            string input = Console.ReadLine();
            var tower = gameManager.FindTower(input);

            if (tower != null)
            {
                Console.WriteLine("\n--- 타워 상세 정보 ---");
                Console.WriteLine($"ID: {tower.ID}");
                Console.WriteLine($"Name: {tower.Name}, Type: {tower.Type}");
                Console.WriteLine($"Level (H/A/R): {tower.HealthLevel}/{tower.AttackLevel}/{tower.RangeLevel}");
                Console.WriteLine($"체력 (Base/Bonus/Current): {tower.BaseHealth}/{tower.BonusHealth}/{tower.CurrentHealth}");
                Console.WriteLine($"공격력 (Base/Bonus/Current): {tower.BaseAttackPower}/{tower.BonusAttackPower}/{tower.CurrentAttackPower}");
                Console.WriteLine($"범위 (Base/Current): {tower.BaseRange}/{tower.CurrentRange}");
            }
            else
            {
                Console.WriteLine(">> [오류] 해당 식별자의 타워를 찾을 수 없습니다.");
            }
        }

        static void RemoveTower()
        {
            Console.Write("삭제할 타워의 ID(앞 8자리) 또는 번호(목록)를 입력하세요: ");
            string input = Console.ReadLine();
            var tower = gameManager.FindTower(input);

            if (tower != null && gameManager.RemoveTower(tower.ID))
            {
                Console.WriteLine($"\n>> [성공] 타워 {tower.ID.ToString().Substring(0, 8)} 삭제 완료.");
            }
            else
            {
                Console.WriteLine(">> [오류] 타워 삭제 실패. ID 또는 번호를 확인하세요.");
            }
        }

        static void ChangeTowerType()
        {
            Console.Write("타입 변경할 타워의 ID(앞 8자리) 또는 번호(목록)를 입력하세요: ");
            string input = Console.ReadLine();
            var tower = gameManager.FindTower(input);

            if (tower == null)
            {
                Console.WriteLine(">> [오류] 해당 타워를 찾을 수 없습니다.");
                return;
            }

            Console.Write($"변경할 새 타워 타입 (Melee, Ranged, Support)을 입력하세요: ");
            if (Enum.TryParse(Console.ReadLine(), true, out TowerType newType))
            {
                if (gameManager.ChangeTowerType(tower.ID, newType))
                {
                    Console.WriteLine($"\n>> [성공] 타워 {tower.ID.ToString().Substring(0, 8)}의 타입이 {tower.Type}에서 {newType}으로 변경 완료.");
                }
                else
                {
                    Console.WriteLine(">> [오류] 타워 타입 변경 실패.");
                }
            }
            else
            {
                Console.WriteLine(">> [오류] 유효하지 않은 타워 타입입니다.");
            }
        }

        static void AddTower()
        {
            Console.Write("추가할 타워 타입 (Melee, Ranged, Support)을 입력하세요: ");
            if (Enum.TryParse(Console.ReadLine(), true, out TowerType newType))
            {
                gameManager.AddTower(newType);
                Console.WriteLine($"\n>> [성공] {newType} 타입 타워 추가 완료. 현재 총 {gameManager.Towers.Count}개.");
            }
            else
            {
                Console.WriteLine(">> [오류] 유효하지 않은 타워 타입입니다.");
            }
        }


        // 요구사항 9.b: 적 목록 테스트
        static void EnemyTestMenu()
        {
            Console.WriteLine("\n### 2. 적 목록 테스트");
            var (count, list) = gameManager.GetEnemyListInfo();
            Console.WriteLine($"[결과] 총 적 갯수: {count}개");
            Console.WriteLine("--- 적 목록 (출현 순서, ID 앞 8자리, 좌표) ---");
            Console.WriteLine(list);
            Console.WriteLine("------------------------------------------------\n");

            Console.WriteLine("b1. 적 삭제 (ID/번호)");
            Console.WriteLine("b2. 적 추가 (Type, 순서, 좌표)");
            Console.Write(">> 세부 메뉴를 선택하세요 (또는 Enter로 돌아가기): ");
            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "b1": RemoveEnemy(); break;
                case "b2": AddEnemy(); break;
            }
        }

        static void RemoveEnemy()
        {
            Console.Write("삭제할 적의 ID(앞 8자리) 또는 번호(목록)를 입력하세요: ");
            string input = Console.ReadLine();
            var enemy = gameManager.FindEnemy(input);

            if (enemy != null && gameManager.RemoveEnemy(enemy.ID))
            {
                Console.WriteLine($"\n>> [성공] 적 {enemy.ID.ToString().Substring(0, 8)} 삭제 완료.");
            }
            else
            {
                Console.WriteLine(">> [오류] 적 삭제 실패. ID 또는 번호를 확인하세요.");
            }
        }

        static void AddEnemy()
        {
            Console.Write("추가할 적 타입 (Normal, MidBoss, FinalBoss)을 입력하세요: ");
            if (Enum.TryParse(Console.ReadLine(), true, out EnemyType newType))
            {
                Console.Write("출현 순서 (정수)를 입력하세요: ");
                if (int.TryParse(Console.ReadLine(), out int spawnOrder))
                {
                    Console.Write("출현 좌표 X를 입력하세요: ");
                    if (int.TryParse(Console.ReadLine(), out int x))
                    {
                        Console.Write("출현 좌표 Y를 입력하세요: ");
                        if (int.TryParse(Console.ReadLine(), out int y))
                        {
                            gameManager.AddEnemy(newType, spawnOrder, (x, y));
                            Console.WriteLine($"\n>> [성공] {newType} 타입 적 (순서: {spawnOrder}, 좌표: ({x},{y})) 추가 완료.");
                            return;
                        }
                    }
                }
            }
            Console.WriteLine(">> [오류] 유효하지 않은 입력 값입니다.");
        }


        // 요구사항 9.c: 객체 Level 변경
        static void LevelChangeMenu()
        {
            Console.WriteLine("\n### 3. 객체 Level 변경 (공격력 레벨만)");
            Console.WriteLine("c1. 플레이어 타워 공격력 레벨 변경");
            Console.WriteLine("c2. 적 공격력 레벨 변경");
            Console.Write(">> 세부 메뉴를 선택하세요: ");
            string choice = Console.ReadLine().ToLower();

            bool isTower = (choice == "c1");
            string unitType = isTower ? "타워" : "적";

            if (choice != "c1" && choice != "c2")
            {
                Console.WriteLine(">> [오류] 잘못된 메뉴 선택입니다.");
                return;
            }

            Console.Write($"변경할 {unitType}의 ID(앞 8자리) 또는 번호(목록)를 입력하세요: ");
            string input = Console.ReadLine();

            BaseUnit unit = isTower ? gameManager.FindTower(input) : (BaseUnit)gameManager.FindEnemy(input);
            Guid targetId = unit?.ID ?? Guid.Empty;

            if (targetId == Guid.Empty)
            {
                Console.WriteLine($">> [오류] 유효하지 않은 {unitType} ID 또는 번호입니다.");
                return;
            }

            Console.Write($"현재 {unitType} 공격력 레벨: {unit.AttackLevel}. 변경할 Level 변화량 (+ 또는 - 값)을 입력하세요: ");
            if (int.TryParse(Console.ReadLine(), out int levelChange))
            {
                if (gameManager.ChangeAttackLevel(targetId, levelChange, isTower))
                {
                    Console.WriteLine($"\n>> [성공] {unitType} {targetId.ToString().Substring(0, 8)}의 공격력 레벨 {levelChange}만큼 변경 완료.");
                    Console.WriteLine($"       새로운 공격력 레벨: {gameManager.FindTower(targetId.ToString())?.AttackLevel ?? gameManager.FindEnemy(targetId.ToString())?.AttackLevel}");
                }
                else
                {
                    Console.WriteLine($">> [오류] {unitType} Level 변경 실패.");
                }
            }
            else
            {
                Console.WriteLine(">> [오류] 유효하지 않은 Level 변화량입니다.");
            }
        }
    }
}