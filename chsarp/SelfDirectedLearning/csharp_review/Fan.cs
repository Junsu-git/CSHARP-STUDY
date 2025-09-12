
namespace reviewLib
{
    class Fan
    {
        // fields for Fan
        POWER_STATE power;
        SWING_STATE swing;
        SPEED_STATE speed;

        // Properties that considered capsuliizing
        int Index { get; }

        public POWER_STATE Power
        {
            get => power;
            set
            {
                power = value;
                if (power == POWER_STATE.POWER_OFF)
                {
                    Speed = SPEED_STATE.SPEED_LV0;
                    Swing = SWING_STATE.SWING_OFF;
                }
            }
        }
        public SWING_STATE Swing
        {
            get => swing;
            set => swing = value;
        }
        public SPEED_STATE Speed
        {
            get => speed;
            set => speed = value;
        }
        // defined enum variables
        public enum POWER_STATE { POWER_ON, POWER_OFF }
        public enum SWING_STATE { SWING_ON, SWING_OFF }
        // POWER_STATE is POWER_ON and SPEED_STATE is SPEED_LV0
        // It means that the power is on but motor is not rotating
        public enum SPEED_STATE { SPEED_LV0, SPEED_LV1, SPEED_LV2, SPEED_LV3 }

        public Fan(int _index)
        {
            this.Index = _index;
            this.Power = POWER_STATE.POWER_OFF;
            this.Swing = SWING_STATE.SWING_OFF;
            this.Speed = SPEED_STATE.SPEED_LV0;
        }

        public void Apply(Fan _fan)
        {
            this.power = _fan.Power;
            this.Swing = _fan.Swing;
            this.Speed = _fan.Speed;
        }
    }
}

//private void ChangePowerState()
//{
//    if (!IsSwitchOn()) return; 
//    bool isSelectionLoop = true;
//    while (isSelectionLoop)
//    {
//        Console.WriteLine("선풍기 속도 변경 메뉴를 입력하세요! (1. 미풍 | 2. 약풍 | 3. 강풍 | 0. 바람 속도 X)");
//        Console.Write("입력: ");
//        int uInput = int.Parse(Console.ReadLine());
//        switch (uInput)
//        {
//            case (int)SPEED_LEVEL.SPEED_LV0:
//                this.speed = (int)SPEED_LEVEL.SPEED_LV0;
//                isSelectionLoop = false;
//                break;
//            case (int)SPEED_LEVEL.SPEED_LV1:
//                this.speed = (int)SPEED_LEVEL.SPEED_LV1;
//                isSelectionLoop = false;
//                break;
//            case (int)SPEED_LEVEL.SPEED_LV2:
//                this.speed = (int)SPEED_LEVEL.SPEED_LV2;
//                isSelectionLoop = false;
//                break;
//            case (int)SPEED_LEVEL.SPEED_LV3:
//                this.speed = (int)SPEED_LEVEL.SPEED_LV3;
//                isSelectionLoop = false;
//                break;
//            default:
//                Console.WriteLine("입력한 설정이 존재하지 않습니다.");
//                continue;
//        }
//    }
//}

//// 전원 여부 검색 함수
//private bool IsSwitchOn()
//{
//    if(this.isTurnOn) return true;
//    else
//    {
//        Console.WriteLine("현재 해당 선풍기의 전원이 꺼져있습니다.");
//        Console.WriteLine("상태가 변경되지 않습니다.");
//        return false;
//    }
//}

//private void ChangeSwingState()
//{
//    if (!IsSwitchOn()) return;
//    bool isSelectionLoop = true;
//    while (isSelectionLoop)
//    {
//        Console.WriteLine("선풍기 회전 메뉴를 입력하세요! (1. 회전 O | 0. 회전 X)");
//        Console.Write("입력: ");
//        int uInput = int.Parse(Console.ReadLine());
//        if (uInput == 0)
//        {
//            this.isSwing = false;
//            Console.WriteLine("선풍기 회전 Off");
//            isSelectionLoop = false;
//        }
//        else if (uInput == 1)
//        {
//            this.isSwing = true;
//            Console.WriteLine("선풍기 회전 On");
//            isSelectionLoop = false;
//        }
//        else
//        {
//            Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
//            continue;
//        }
//    }
//}

//private void ChangOnOffState()
//{
//    bool isSelectionLoop = true;
//    while (isSelectionLoop)
//    {
//        Console.WriteLine("선풍기 전원 상태를 입력하세요. (1. 전원 On | 0. 전원 Off)");
//        Console.Write("입력: ");
//        int uInput = int.Parse(Console.ReadLine());
//        if (uInput == 0)
//        {
//            this.isTurnOn = false;
//            Console.WriteLine("선풍기 전원 Off");
//            isSelectionLoop = false;
//        }
//        else if (uInput == 1)
//        {
//            this.isTurnOn = true;
//            Console.WriteLine("선풍기 전원 On");
//            isSelectionLoop = false;
//        }
//        else
//        {
//            Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
//            continue;
//        }
//    }
//}