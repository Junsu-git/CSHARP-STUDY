
using System.Net.NetworkInformation;

namespace csharp_005_task
{
    internal class Circulator
    {
        int Index { get; set; }     // 읽고 쓰기 가능해야함
        PWR_MENU Power { 
            get;
            set
            {
                Power = value;
                if (Power == PWR_MENU.Off)
                {
                    // 바람 속도 0
                    // 
                }
            }
        }    

        enum PWR_MENU {On, Eco, Off};

        // 파워? 메뉴 형태로 존재해도 됨.
        // 바람 스피드 = 인트 형태로 존재해도 됨
        // 바람 모드? 메뉴 형태로 존재해도 됨
        // 
        //POWER_STATUS power;
        //public int Index
        //{
        //    get => index;
        //}

        //public SWING_STATUS Swing { get; set; }
        //public WIND_MODE Mode { get; set; }
        //public POWER_STATUS Power
        //{
        //    get => power;
        //    set
        //    {
        //        power = value;
        //        if (power == POWER_STATUS.Off)
        //        {
        //            Swing = SWING_STATUS.Off;
        //            Mode = WIND_MODE.NORMAL;
        //        }
        //    }
        //}


        //public enum WIND_MODE { NORMAL, SLEEP, NATURAL } // Natural 현실 바람처럼 알아서 바뀌는 모드
        //public enum POWER_STATUS { On, Off, Eco }
        //public enum SWING_STATUS { On, Off }


        //enum PRINT_TYPE { POEWR, SWING, WINDMODE, WINSPEED, WINLENGTH}
        public Circulator(int index)
        {
            this.Index = index;
        }

        public void PrintIndex()
        {
            Console.WriteLine(Index);
        }
        //internal void DisplayStatus()
        //{
        //    Console.WriteLine();
        //}

        //internal void SetPower()
        //{
        //    throw new NotImplementedException();
        //}


        //// 타이머는 켜고 끌 때만 사용
        //internal void SetTimer(int v)
        //{
        //    throw new NotImplementedException();
        //}

        //internal void SetWindMode(WIND_MODE wm)
        //{
        //    Mode = wm;
        //}

        //internal void SetWindSpeed(int v)
        //{
        //    throw new NotImplementedException();
        //}

        //internal void SetSwing(SWING_STATUS value)
        //{
        //    throw new NotImplementedException();
        //}

        //void PirntInfo(PRINT_TYPE pt) 
        //{ 
        //    switch (pt)
        //    {
        //        case PRINT_TYPE.POEWR:
        //            break;
        //            case PRINT_TYPE.SWING:

        //    }

        //}
    }
}