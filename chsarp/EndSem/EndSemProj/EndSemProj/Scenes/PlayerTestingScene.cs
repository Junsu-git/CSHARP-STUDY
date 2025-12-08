using EndSemProj.Core;
using EndSemProj.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndSemProj.Scenes
{
    // 2. 시뮬레이션(전투) 화면
    class PlayerTestingScene : Scene
    {
        private Player p1, p2;
        private GameLogger logger;

        // 메뉴 관련 상태
        private enum MenuState { Root, ScopeSelect, TargetSelect, ActionSelect, WeaponList }
        private MenuState currentMenuState = MenuState.Root;
        private List<string> menuItems = new List<string>();
        private int menuIndex = 0;

        // 임시 저장 데이터
        private bool isTargetAll;
        private Player targetPlayer;

        public override void Enter()
        {
            Console.Clear();
            p1 = new Player("플레이어 1", 100, "RustySword");
            p2 = new Player("플레이어 2", 80, "WoodenStaff");
            logger = new GameLogger(12);
            UpdateMenuList(); // 초기 메뉴 로드
        }

        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);

            // 상단: 플레이어 정보
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================= BATTLE STATUS =======================".PadRight(90));
            PrintPlayerInfo(p1);
            PrintPlayerInfo(p2);
            Console.WriteLine("=============================================================".PadRight(90));
            Console.ResetColor();

            // 중단: 로그
            logger.DrawLogs(0, 4);

            // 구분선
            Console.SetCursorPosition(0, 17);
            Console.WriteLine("-------------------------------------------------------------".PadRight(90));

            // 하단: 메뉴
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($" [ MENU > {currentMenuState} ]".PadRight(90));
            Console.ResetColor();

            for (int i = 0; i < 5; i++)
            {
                if (i < menuItems.Count)
                {
                    if (i == menuIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($" >> {menuItems[i]}".PadRight(90));
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"    {menuItems[i]}".PadRight(90));
                    }
                }
                else Console.WriteLine("".PadRight(90));
            }
        }

        private void PrintPlayerInfo(Player p)
        {
            Console.WriteLine($" [{p.Name}] Lv.{p.Level} | HP: {p.HP} | ATK: {p.Attack} | WPN: {p.WeaponName} | Pos: ({p.X},{p.Y})".PadRight(90));
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            // 1. 메뉴 조작 (화살표, 엔터)
            if (key.Key == ConsoleKey.UpArrow) { menuIndex--; if (menuIndex < 0) menuIndex = menuItems.Count - 1; }
            else if (key.Key == ConsoleKey.DownArrow) { menuIndex++; if (menuIndex >= menuItems.Count) menuIndex = 0; }
            else if (key.Key == ConsoleKey.Enter) { ProcessMenuSelect(); }

            // 2. 캐릭터 조작 (WASD, IJKL) - 로직 분리
            else ProcessGameControl(key);
        }

        private void ProcessGameControl(ConsoleKeyInfo key)
        {
            string log = "";
            switch (key.Key)
            {
                // P1
                case ConsoleKey.W: log = p1.Move(0, -1); break;
                case ConsoleKey.A: log = p1.Move(-1, 0); break;
                case ConsoleKey.S: log = p1.Move(0, 1); break;
                case ConsoleKey.D: log = p1.Move(1, 0); break;
                case ConsoleKey.F: log = $"[P1] 공격! 데미지 {p1.Attack}"; break;
                case ConsoleKey.G: log = $"[P1] 스킬 사용!"; break;

                // P2
                case ConsoleKey.I: log = p2.Move(0, -1); break;
                case ConsoleKey.J: log = p2.Move(-1, 0); break;
                case ConsoleKey.K: log = p2.Move(0, 1); break;
                case ConsoleKey.L: log = p2.Move(1, 0); break;
            }
            if (key.KeyChar == ';') log = $"[P2] 공격! 데미지 {p2.Attack}";
            if (key.KeyChar == '\'') log = $"[P2] 스킬 사용!";

            if (!string.IsNullOrEmpty(log)) logger.Add(log);
        }

        // --- 메뉴 로직 (상태 패턴 비슷하게 처리) ---
        private void ProcessMenuSelect()
        {
            string selected = menuItems[menuIndex];

            if (selected == "< 뒤로") // 뒤로가기 공통 처리
            {
                if (currentMenuState == MenuState.WeaponList) currentMenuState = MenuState.ActionSelect;
                else if (currentMenuState == MenuState.ActionSelect) currentMenuState = isTargetAll ? MenuState.ScopeSelect : MenuState.TargetSelect;
                else if (currentMenuState == MenuState.TargetSelect) currentMenuState = MenuState.ScopeSelect;
                else if (currentMenuState == MenuState.ScopeSelect) currentMenuState = MenuState.Root;

                UpdateMenuList();
                return;
            }

            switch (currentMenuState)
            {
                case MenuState.Root:
                    if (menuIndex == 0) ChangeState(MenuState.ScopeSelect);
                    else Program.SceneMgr.LoadScene(new TitleScene()); // 메인으로 나가기
                    break;
                case MenuState.ScopeSelect:
                    isTargetAll = (menuIndex == 1);
                    ChangeState(isTargetAll ? MenuState.ActionSelect : MenuState.TargetSelect);
                    break;
                case MenuState.TargetSelect:
                    targetPlayer = (menuIndex == 0) ? p1 : p2;
                    ChangeState(MenuState.ActionSelect);
                    break;
                case MenuState.ActionSelect:
                    if (menuIndex == 0) HandleLevelUp();
                    else ChangeState(MenuState.WeaponList);
                    break;
                case MenuState.WeaponList:
                    ApplyWeaponChange(selected);
                    ChangeState(MenuState.Root);
                    break;
            }
        }

        private void HandleLevelUp()
        {
            Console.SetCursorPosition(0, 25);
            Console.Write(" >> 레벨 증가량 입력: ".PadRight(20));
            Console.CursorVisible = true;
            if (int.TryParse(Console.ReadLine(), out int amt))
            {
                if (isTargetAll) { p1.LevelUp(amt); p2.LevelUp(amt); logger.Add($"[All] 레벨 {amt} 증가"); }
                else { targetPlayer.LevelUp(amt); logger.Add($"[{targetPlayer.Name}] 레벨 {amt} 증가"); }
            }
            Console.CursorVisible = false;
            ChangeState(MenuState.Root);
        }

        private void ApplyWeaponChange(string weapon)
        {
            if (isTargetAll) { p1.ChangeWeapon(weapon); p2.ChangeWeapon(weapon); logger.Add($"[All] 무기변경 -> {weapon}"); }
            else { targetPlayer.ChangeWeapon(weapon); logger.Add($"[{targetPlayer.Name}] 무기변경 -> {weapon}"); }
        }

        private void ChangeState(MenuState nextState)
        {
            currentMenuState = nextState;
            UpdateMenuList();
        }

        private void UpdateMenuList()
        {
            menuItems.Clear();
            menuIndex = 0;
            switch (currentMenuState)
            {
                case MenuState.Root:
                    menuItems.AddRange(new[] { "상태 변경", "메인 화면으로 나가기" });
                    break;
                case MenuState.ScopeSelect:
                    menuItems.AddRange(new[] { "단일 대상", "전체 대상", "< 뒤로" });
                    break;
                case MenuState.TargetSelect:
                    menuItems.AddRange(new[] { "P1 (전사)", "P2 (법사)", "< 뒤로" });
                    break;
                case MenuState.ActionSelect:
                    menuItems.AddRange(new[] { "레벨 업", "무기 변경", "< 뒤로" });
                    break;
                case MenuState.WeaponList:
                    foreach (var key in DataRepository.Weapons.Keys) menuItems.Add(key);
                    menuItems.Add("< 뒤로");
                    break;
            }
        }
    }
}
