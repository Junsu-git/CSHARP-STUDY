namespace Chap7.MethodHiding
{
    internal class Derived : Base
    {
        public new void MyMethod()
        {
            Console.WriteLine("Derived.MyMethod()");
        }
    }
}