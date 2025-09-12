
using System;

namespace Train1
{
    internal class Q1
    {
        internal static void RUN()
        {
            Console.WriteLine("사각형의 너비를 입력하세요.");
            int width = int.Parse(Console.ReadLine());
            Console.WriteLine("사각형의 높이를 입력하세요.");
            int height = int.Parse(Console.ReadLine());

            Console.WriteLine($"입력하신 사각형의 너비: {width * height}");

        }
    }
}