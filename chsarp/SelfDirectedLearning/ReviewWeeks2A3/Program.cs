using reviewLib;

namespace reviewMain
{
    internal class Program
    {
        // enum variagble for defined projects
        enum PROGRAM_LIST { QUIT, FAN };
        static void Main(string[] args)
        {
            Run();
        }

        // totac for enter each defined project
        private static void Run()
        {
            // using endless loop to make user restart program when they finish each project
            while (true)
            {
                Console.Clear();
                // Print welcome menu
                PrintMenu();
                // Get user input via InputHandler.GetMenuInput and enter each branchs using switch case
                switch (InputHandler.GetMenuInput<PROGRAM_LIST>())
                {
                    case PROGRAM_LIST.QUIT:
                        QuitProgram();
                        break;
                    case PROGRAM_LIST.FAN:
                        new FanConsoleUI().Run();
                        break;
                    default:
                        Console.WriteLine("존재하지 않는 번호입니다. 다시 입력해주세요.\n");
                        continue;
                }
            }
        }

        // Print Menu when user active program
        private static void PrintMenu()
        {
            Console.WriteLine("========= 메인 프로그램 시작 =========\n");
            Console.WriteLine("실행가능 프로그램 목록");
            Console.WriteLine("1. 선풍기 관리 프로그램\n");
            Console.WriteLine("0. 종료\n");
        }

        // Quit program
        private static void QuitProgram()
        {
            Console.WriteLine("========= 프로그램 종료 =========");
            Environment.Exit(1);
        }
    }
}