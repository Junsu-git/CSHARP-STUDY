using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chap7.ExtentionMethod
{
    internal static class StringExtention
    {
        public static string Append(this string str1, string str2)
        {
            str1 += str2;
            return str1;
        }
    }
    public static class Vitamin7_2
    {
        public static void Run()
        {
            string hello = "Hello";
            Console.WriteLine(hello.Append(", vello"));
        }
    }
}
