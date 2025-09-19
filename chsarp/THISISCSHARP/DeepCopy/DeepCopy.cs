namespace Chap7
{
    public class MyClass
    {
        public int myField1;
        public int myField2;

        public MyClass DeepCopy()
        {
            MyClass newCopy = new MyClass();
            newCopy.myField1 = myField1;
            newCopy.myField2 = myField2;
            return newCopy;
        }
    }

    public class CopyActiver
    {
        public static void Run()
        {
            Console.WriteLine("Shallow Copy");
            {
                MyClass source = new MyClass();
                source.myField1 = 10;
                source.myField2 = 20;

                MyClass target = source;
                target.myField2 = 30;

                Console.WriteLine($"{source.myField1}, {source.myField2}");
                Console.WriteLine($"{target.myField1}, {target.myField2}");
            }
            Console.WriteLine("Deep Copy");

            {
                MyClass source = new MyClass();
                source.myField1 = 10;
                source.myField2 = 20;

                MyClass target = source.DeepCopy() ;
                target.myField2 = 30;

                Console.WriteLine($"{source.myField1}, {source.myField2}");
                Console.WriteLine($"{target.myField1}, {target.myField2}");
            }
            Console.WriteLine("====================");
        }
    }
}
