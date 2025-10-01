namespace Chap7.PartialClass
{
    internal class PartialClass
    {
        partial class MyClass
        {
            public void Method1()
            {
                Console.WriteLine("Method");
            }
            public void Method2()
            {
                Console.WriteLine("Method2");
            }
        }
        partial class MyClass
        {
            public void Method3()
            {
                Console.WriteLine("Method3");
            }
            public void Method4()
            {
                Console.WriteLine("Method4");
            }
        }
        public static void Run()
        {
            MyClass myClass = new MyClass();
            myClass.Method1();
            myClass.Method2();
            myClass.Method3();
            myClass.Method4();
        }
    }
}
