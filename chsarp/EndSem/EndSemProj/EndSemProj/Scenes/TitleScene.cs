using EndSemProj.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndSemProj.Scenes
{
    // 1. 메인 메뉴 화면 (가장 먼저 실행됨)
    class TitleScene : Scene
    {
        private string[] options = { "시뮬레이션 시작", "종료" };
        private int selectedIndex = 0;

        public override void Enter()
        {
            Console.Clear();
            Console.CursorVisible = false;
        }

        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("\n\n\n");
            Console.WriteLine("        ========================================");
            Console.WriteLine("             G A M E   S I M U L A T O R        ");
            Console.WriteLine("        ========================================");
            Console.WriteLine("\n");

            for (int i = 0; i < options.Length; i++)
            {
                string prefix = (i == selectedIndex) ? " >> " : "    ";
                Console.ForegroundColor = (i == selectedIndex) ? ConsoleColor.Green : ConsoleColor.Gray;
                Console.WriteLine($"{prefix}{options[i]}".PadRight(40));
                Console.ResetColor();
            }
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = Math.Max(0, selectedIndex - 1);
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = Math.Min(options.Length - 1, selectedIndex + 1);
                    break;
                case ConsoleKey.Enter:
                    if (selectedIndex == 0) Program.SceneMgr.LoadScene(new PlayerTestingScene());
                    else Environment.Exit(0);
                    break;
            }
        }
    }
}
