namespace reviewLib.Fan
{
    public static class InputHandler
    {        
        public static T GetMenuInput<T>() where T : Enum
        {
            while (true)
            {
                Console.Write("입력: ");
                if (int.TryParse(Console.ReadLine(), out int input) && Enum.IsDefined(typeof(T), input))
                    return (T)(object)input;
                Console.Write("존재하지 않는 메뉴 입력\n");
            }
        }
        public static int GetIntInput()
        {
            while (true)
            {
                Console.Write("입력: ");
                if (int.TryParse(Console.ReadLine(), out int uInput))
                    return uInput;
                Console.WriteLine("잘못된 타입 입력\n");
            }
        }
    }
}