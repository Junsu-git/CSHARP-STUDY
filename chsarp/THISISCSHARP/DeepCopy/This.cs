namespace Chap7
{
    public class Employee
    {
        string name;
        string position;

        public void SetName(string name)
        {
            this.name = name;
        }
        public object GetPosittion()
        {
            return this.position;
        }

        public object GetName()
        {
            return this.name;
        }

        public void SetPosition(string v)
        {
            this.position = v;
        }

        // 원래는 클래스의 실행은 추출해야하는데 귀찮아 ㅇㅅㅇ;;
        public static void Run()
        {
            Employee pooh = new Employee();
            pooh.SetName("Pooh");
            pooh.SetPosition("Waiter");
            Console.WriteLine($"{pooh.GetName()} {pooh.GetPosittion()}");

            Employee tigger = new Employee();
            tigger.SetName("Tigger");
            tigger.SetPosition("Cleaner");
            Console.WriteLine($"{tigger.GetName()} {tigger.GetPosittion()}");
        }
    }
    

    public class ThisConstructor
    {
        int a, b, c;

        public ThisConstructor()
        {
            this.a = 5425;
            Console.WriteLine($"ThisConstructor()");
        }
        public ThisConstructor(int b) : this()
        {
            this.b = b;
            Console.WriteLine($"ThisConstructor({b})");
        }
        public ThisConstructor(int b, int c) : this(b)
        {
            this.c = c;
            Console.WriteLine($"ThisConstructor({b}, {c})");
        }

        public void PrintFields()
        {
            Console.WriteLine($"a:{a}, b:{b}, c:{c}");
        }
        
        // 여기도
        public static void Run()
        {
            ThisConstructor a = new ThisConstructor();

            a.PrintFields();
            Console.WriteLine();

            ThisConstructor b = new ThisConstructor(1);
            b.PrintFields();
            Console.WriteLine();

            ThisConstructor c = new ThisConstructor(10,20); 
            c.PrintFields();
            Console.WriteLine("====================");
        }

    }
}
