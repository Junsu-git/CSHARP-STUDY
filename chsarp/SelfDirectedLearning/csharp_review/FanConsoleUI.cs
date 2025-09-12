


namespace reviewLib
{
    public class FanConsoleUI
    {
        FanManager fm = new FanManager();
        enum MENULIST { QUIT, CREATE, CHANGE, PRINT }
        enum CREATELIST { RETURN, SINGLE_CREATE, MULTIPLE_CREATE }
        enum CHANGELIST { RETURN, SINGLE_CHANGE, MULTIPLE_CHANGE }
        enum PRINTLIST { RETURN, SINGLE_PRINT, MULTIPLE_PRINT }

        public void Run()
        {
            PrintWelcome();
            while (true)
            {
                PrintFanMenu();
                SelectFanMenu();
            }
            
        }

        // 프로그램 실행 시, 띄우는 환영 인사
        private void PrintWelcome()
        {
            Console.WriteLine("선풍기 프로그램 실행\n");
        }

        private void PrintFanMenu()
        {
            Console.WriteLine($"현재 생성 된 선풍기 수: {fm.GetListLength}개\n");
            Console.WriteLine("======= 메뉴를 선택 =======");
            Console.WriteLine($"1. 선풍기 생성");
            Console.WriteLine($"2. 선풍기 상태 변경");
            Console.WriteLine($"3. 선풍기 출력\n");
            Console.WriteLine($"0. 선풍기 프로그램 종료\n");
        }

        private void SelectFanMenu()
        {
            switch (InputHandler.GetMenuInput<MENULIST>())
            {
                // 프로그램 종료
                case MENULIST.QUIT:
                    QuitProgram();
                    return;

                case MENULIST.CREATE:
                    SelectCreateMenu();
                    break;
                case MENULIST.CHANGE:
                    SelectChangeMenu();
                    break;
                case MENULIST.PRINT:
                    SelectPrintMenu();
                    break;
            }
        }

        private void SelectCreateMenu()
        {
            switch (InputHandler.GetMenuInput<CREATELIST>())
            {
                // 프로그램 종료
                case CREATELIST.RETURN:
                    return;

                case CREATELIST.SINGLE_CREATE:
                    fm.CreateFan();
                    break;
                case CREATELIST.MULTIPLE_CREATE:
                    Console.WriteLine("선풍기 다중 생성 메뉴 선택");
                    Console.WriteLine("생성할 선풍기 수 입력\n");
                    for (int i = 0; i < InputHandler.GetAmountInput(); i++)
                        fm.CreateFan();
                    break;
            }
        }

        private void SelectChangeMenu()
        {
            switch (InputHandler.GetMenuInput<CHANGELIST>())
            {
                // 프로그램 종료
                case CHANGELIST.RETURN:
                    return;

                case CHANGELIST.SINGLE_CHANGE:
                    SingleFanChange();
                    break;
                case CHANGELIST.MULTIPLE_CHANGE:
                    MultipleFanChange();
                    break;
            }
        }
        private void SingleFanChange()
        {

            
        }
        private void MultipleFanChange()
        {
            throw new NotImplementedException();
        }


        private void SelectPrintMenu()
        {
            switch (InputHandler.GetMenuInput<PRINTLIST>())
            {
                // 프로그램 종료
                case PRINTLIST.RETURN:
                    return;

                case PRINTLIST.SINGLE_PRINT:

                    break;
                case PRINTLIST.MULTIPLE_PRINT:

                    break;
            }
        }


        private void QuitProgram()
        {
            Console.WriteLine("========= 선풍기 프로그램 종료 =========\n");
        }

    }
}