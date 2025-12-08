using System;

namespace ShootingGameLib
{
    public static class Util
    {
        // 키 버퍼 비우기
        public static void ClearKeyBuffer()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        // 특정 줄 지우기
        public static void ClearLine(int y)
        {
            if (y < 0 || y >= Console.WindowHeight) return;
            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, y);
        }

        // [신규 이동] ESC 지원 입력 함수 (공용화)
        public static string ReadInputWithCancel()
        {
            string input = "";
            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return null; // 취소 신호
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(); // 줄바꿈
                    return input;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                        Console.Write("\b \b"); // 화면에서 지우기
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
        }
    }
}