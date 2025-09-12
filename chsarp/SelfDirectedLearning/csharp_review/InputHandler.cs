namespace reviewLib
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
                Console.Write("잘못 된 입력\n");
            }
        }
        public static int GetAmountInput()
        {
            while (true)
            {
                Console.Write("입력: ");
                if (int.TryParse(Console.ReadLine(), out int uInput))
                    return uInput;
                Console.WriteLine("잘못된 입력\n");
            }
        }
    }
}
