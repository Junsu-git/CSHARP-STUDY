
using System.Net.NetworkInformation;

namespace csharp_005_task
{
    internal class Circulator
    {
        int index = 0;
        POWER_STATUS power;
        public int Index
        {
            get => index;
        }

        public SWING_STATUS Swing { get; set; }
        public WIND_MODE Mode { get; set; }
        public POWER_STATUS Power
        {
            get => power;
            set
            {
                power = value;
                if (power == POWER_STATUS.Off)
                {
                    Swing = SWING_STATUS.Off;
                    Mode = WIND_MODE.NORMAL;
                }
            }
        }


        public enum WIND_MODE { NORMAL, SLEEP, NATURAL } // Natural 현실 바람처럼 알아서 바뀌는 모드
        public enum POWER_STATUS { On, Off, Eco }
        public enum SWING_STATUS { On, Off }


        enum PRINT_TYPE { POEWR, SWING, WINDMODE, WINSPEED, WINLENGTH}
        public Circulator()
        {
            this.index = index++;
        }
        internal void DisplayStatus()
        {
            Console.WriteLine();
        }

        internal void SetPower()
        {
            throw new NotImplementedException();
        }


        // 타이머는 켜고 끌 때만 사용
        internal void SetTimer(int v)
        {
            throw new NotImplementedException();
        }

        internal void SetWindMode(WIND_MODE wm)
        {
            Mode = wm;
        }

        internal void SetWindSpeed(int v)
        {
            throw new NotImplementedException();
        }

        internal void SetSwing(SWING_STATUS value)
        {
            throw new NotImplementedException();
        }

        void PirntInfo(PRINT_TYPE pt) 
        { 
            switch (pt)
            {
                case PRINT_TYPE.POEWR:
                    break;
                    case PRINT_TYPE.SWING:

            }

        }
    }
}