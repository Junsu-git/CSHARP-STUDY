using System.Data;
using System.Reflection.Metadata;
using System.Runtime.Versioning;

namespace reviewLib
{
    public class FanConsoleUI
    {
        bool isDone = false;
        FanManager fm = new FanManager();
        enum MENULIST { QUIT, CREATE, CHANGE, PRINT }
        enum CREATELIST { RETURN, SINGLE_CREATE, MULTIPLE_CREATE }
        enum CHANGELIST { RETURN, SINGLE_CHANGE, MULTIPLE_CHANGE }
        enum CHANGEPOWERLIST { QUIT, ON, OFF}
        enum CHANGESPEEDLIST { QUIT, LV0, LV1, LV2, LV3 }
        enum CHANGESWINGLIST { QUIT, ON, OFF }

        enum PRINTLIST { RETURN, SINGLE_PRINT, MULTIPLE_PRINT }

        public void Run()
        {
            Console.Clear();
            PrintWelcome();
            while (!isDone)
            {
                PrintFanMenu();
                SelectFanMenu();
            }
        }

        // 프로그램 실행 시, 띄우는 환영 인사
        private void PrintWelcome()
        {
            Console.WriteLine("===== 선풍기 프로그램 시작 =====\n");
        }

        private void PrintFanMenu()
        {
            Console.WriteLine($"\n현재 생성 된 선풍기 수: {fm.GetListLength}개\n");
            Console.WriteLine("======= 메뉴 선택 =======\n");
            Console.WriteLine($"1. 선풍기 생성");
            Console.WriteLine($"2. 선풍기 상태 변경");
            Console.WriteLine($"3. 선풍기 출력");

            Console.WriteLine();
            Console.WriteLine($"0. 선풍기 프로그램 종료\n");
            Console.WriteLine("=========================\n");
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
                    PrintCreatMenu();
                    SelectCreateMenu();
                    break;
                case MENULIST.CHANGE:
                    if (IsMenuAvailablity()) return;
                    PrintChangeMenu();
                    SelectChangeMenu();
                    break;
                case MENULIST.PRINT:
                    SelectPrintMenu();
                    break;
            }
        }

        private void PrintCreatMenu()
        {
            Console.Clear();
            Console.WriteLine("===== 생성 메뉴 선택 =====\n");
            Console.WriteLine($"1. 단일 생성");
            Console.WriteLine($"2. 다중 생성");

            Console.WriteLine();
            Console.WriteLine($"0.돌아가기\n");
            Console.WriteLine("=========================\n");
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
                    int uInput = InputHandler.GetIntInput();
                    for (int i = 0; i < uInput; i++)
                        fm.CreateFan();
                    break;
            }
            Console.Clear();
        }

        private void PrintChangeMenu()
        {
            Console.Clear();
            Console.WriteLine("===== 변경 메뉴 선택 =====\n");
            Console.WriteLine($"1. 단일 변경"); // in this phase user must input certain index that is in the list
            Console.WriteLine($"2. 전체 변경"); // 

            Console.WriteLine();
            Console.WriteLine($"0.돌아가기\n");
            Console.WriteLine("=========================\n");
        }
        private bool IsMenuAvailablity()
        {
            if (fm.IsListEmpty())
            {
                Console.WriteLine("생성된 선풍기 없음");
                return true;
            }
            return false;
        }

        private void SelectChangeMenu()
        {
            switch (InputHandler.GetMenuInput<CHANGELIST>())
            {
                // 프로그램 종료
                case CHANGELIST.RETURN:
                    return;

                case CHANGELIST.SINGLE_CHANGE:
                    PrintSingleFanChangeMenu();
                    SelectSingleFanChangeMenu();
                    break;
                case CHANGELIST.MULTIPLE_CHANGE:
                    // 여기선 바로 변경 메뉴 시작
                    SelectMultipleFanChangeMenu();
                    break;
            }
        }

        

        private void PrintSingleFanChangeMenu()
        {
            Console.Clear();
            Console.WriteLine("===== 단일 변경 메뉴 선택 =====\n");
            Console.WriteLine("변경할 선풍기 번호 입력");
            Console.WriteLine($"\n현재 생성 된 선풍기 수: {fm.GetListLength}개\n");

            Console.WriteLine();
            Console.WriteLine($"0.돌아가기\n");
            Console.WriteLine("=========================\n");
        }

        private void SelectSingleFanChangeMenu()
        {
            Fan curFan = fm.GetFan(GetValidIndex());
            Fan tempFan = ChangeFan();
            if (tempFan == null)
                return;
            curFan.Apply(tempFan);
        }

        private void SelectMultipleFanChangeMenu()
        {
            Fan tempFan = ChangeFan();
            foreach(fm.)
        }

        // 선풍기 변경 로직 -> change fan 을 통해 임시 팬을 하나 만들고, 그 상태를 fan.ApplyState로 복붙
        // 그럼 change Fan 안에선 3번의 입력을 받아, 선풍기 부품의 각각의 상태를 변경해야함
        // 
        private Fan ChangeFan()
        {
            Fan tempFan = new Fan(-1);
            if (!ChangeFanPower(tempFan)) return null;
            // 전원이 꺼져 있을 떄의 반환 처리
            if (tempFan.Power == Fan.POWER_STATE.POWER_OFF) return null;
            if (!ChangeFanSpeed(tempFan)) return null;
            if (!ChangeFanSwing(tempFan)) return null;

            return tempFan;
        }



        private bool ChangeFanPower(Fan _fan)
        {
            Console.WriteLine("선풍기 전원 메뉴");
            Console.WriteLine("1. 끄기");
            Console.WriteLine("2. 켜기");
            Console.WriteLine("0. 돌아가기");

            switch (InputHandler.GetMenuInput<CHANGEPOWERLIST>())
            {
                case CHANGEPOWERLIST.QUIT: return false;
                case CHANGEPOWERLIST.ON:
                    _fan.Power = Fan.POWER_STATE.POWER_ON; break;
                case CHANGEPOWERLIST.OFF:
                    _fan.Power = Fan.POWER_STATE.POWER_OFF; break;
            }
            return true;
        }
        private bool ChangeFanSpeed(Fan _fan)
        {
            Console.WriteLine("선풍기 속도 변경 메뉴");
            Console.WriteLine("1. 끄기");
            Console.WriteLine("2. 미풍");
            Console.WriteLine("3. 약풍");
            Console.WriteLine("4. 강풍");
            switch (InputHandler.GetMenuInput<CHANGESPEEDLIST>())
            {
                case CHANGESPEEDLIST.QUIT: return false;
                case CHANGESPEEDLIST.LV0:
                    _fan.Speed = Fan.SPEED_STATE.SPEED_LV1; break;
                case CHANGESPEEDLIST.LV1:
                    _fan.Speed = Fan.SPEED_STATE.SPEED_LV1; break;
                case CHANGESPEEDLIST.LV2:
                    _fan.Speed = Fan.SPEED_STATE.SPEED_LV1; break;
                case CHANGESPEEDLIST.LV3:
                    _fan.Speed = Fan.SPEED_STATE.SPEED_LV1; break;
            }
            return true;
        }
        private bool ChangeFanSwing(Fan _fan)
        {

            Console.WriteLine("선풍기 회전 메뉴");
            Console.WriteLine("1. 끄기");
            Console.WriteLine("2. 켜기");
            switch (InputHandler.GetMenuInput<CHANGESWINGLIST>())
            {
                case CHANGESWINGLIST.QUIT: return false;
                case CHANGESWINGLIST.ON:
                    _fan.Swing = Fan.SWING_STATE.SWING_ON;
                    break;
                case CHANGESWINGLIST.OFF:
                    _fan.Swing = Fan.SWING_STATE.SWING_OFF;
                    break;
            }
            return true;
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

        private int GetValidIndex()
        {
            int uInput;
            while (true)
            {
                // 유저의 입력을 받아서
                uInput = InputHandler.GetIntInput() - 1;
                if (fm.IsValidIndex(uInput))
                    return uInput;
                else
                    Console.WriteLine("잘못된 인덱스 값 입력\n");
            }
        }

        private void QuitProgram()
        {
            Console.WriteLine("========= 선풍기 프로그램 종료 =========\n");
            isDone = true;
        }

    }
}