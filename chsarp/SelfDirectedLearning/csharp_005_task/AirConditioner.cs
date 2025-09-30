
namespace csharp_005_task
{
    internal class AirConditioner
    {
        System.Timers.Timer timer;  // 타이머
        string? name;    // 이름
        string? model;   // 모델
        bool isOn;      // 실제 전원
        // 상태 값 변화 프로퍼티
        bool IsOn
        {
            get => isOn;
            set
            {
                isOn = value;
                if (isOn == false)
                {
                    mode = MODE.Off;
                    wind = WIND.Off;
                }
                else
                {
                    mode = MODE.COOLER;
                    wind = WIND.LIGHT;
                }
            }
        }
        MODE mode;      // 기기의 현재 모드
        MODE preMode;   // 기기의 과거 모드
        WIND wind;      // 기기의 현재 바람 상태
        WIND preWind;   // 기기의 과거 바람 상태

        // 정의 된 각 상태 열거
        enum MODE { UNKOWN, COOLER, HEATER, DEHUM, CIRCU, AI, Off }
        enum WIND { UNKNOWN, LEAST, BREEZE, LIGHT, MEDIUM, STRONG, Off }

        // 출력 매개변수를 위한 열거형
        public enum MENU { POWER, MODE, WIND }



        // 생성자
        public AirConditioner(string name = "DEFAULT", string model = "DEFAULT", bool power = false, int sec = 0)
        {
            Init(name, model, power, sec);
        }

        // 생성자 호출 생성 함수
        private void Init(string name, string model, bool power, int sec)
        {
            this.name = name;
            this.model = model;
            IsOn = power;
            if (sec > 0)
            {
                timer = new System.Timers.Timer(sec * 1000);
                timer.Elapsed += (s, e) => AutoPowerOnOff(!IsOn);
                timer.AutoReset = false;
            }
        }

        // 이름 값 받기
        public string GetName()
        {
            return name;
        }

        // 모델 값 받기
        public string GetModel()
        {
            return model;
        }

        // 타이머 이후 자동 전원 변경
        public void AutoPowerOnOff(bool flag)
        {
            if (flag) PowerOn();
            else PowerOff();
        }

        // 전원 켜기
        public void PowerOn()
        {
            IsOn = true;
            PrintChange(MENU.POWER);
        }

        // 전원 끄기
        public void PowerOff()
        {
            IsOn = false;
            PrintChange(MENU.POWER);
        }

        // 전원 상태 받기
        public string GetPower()
        {
            if (IsPowerOn()) return "ON";
            else return "OFF";
        }

        // 전원 예외처리
        public bool IsPowerOn()
        {
            if (IsOn) return true;
            return false;
        }

        // 모드 상태 입력
        public void SetMode(int input)
        {
            if (!IsPowerOn())
            {
                Console.WriteLine("[ WARNING ] POWER IS OFF CANNOT CHANGE STATE!");
                return;
            }
            preMode = mode;
            switch (input)
            {
                case (int)MODE.COOLER:
                    mode = MODE.COOLER; break;
                case (int)MODE.HEATER:
                    mode = MODE.HEATER; break;
                case (int)MODE.DEHUM:
                    mode = MODE.DEHUM; break;
                case (int)MODE.CIRCU:
                    mode = MODE.CIRCU; break;
                case (int)MODE.AI:
                    mode = MODE.AI; break;
                default:
                    mode = MODE.UNKOWN; break;
            }
            PrintChange(MENU.MODE);
        }

        // 모드 상태 받기
        public string GetMode()
        {
            switch (mode)
            {
                case MODE.COOLER: return "COOLER";
                case MODE.HEATER: return "HEATER";
                case MODE.DEHUM: return "DEHUMIDIFIER";
                case MODE.CIRCU: return "CIRCULATOR";
                case MODE.UNKOWN: return "WRONG MODE";
            }
            return "OFF";
        }

        // 바람 상태 변경
        public void SetWind(int input)
        {
            if (!IsPowerOn())
            {
                Console.WriteLine("[ WARNING ] POWER IS OFF CANNOT CHANGE STATE!");
                return;
            }
            preWind = wind;
            switch (input)
            {
                case (int)WIND.LEAST:
                    wind = WIND.LEAST; break;
                case (int)WIND.BREEZE:
                    wind = WIND.BREEZE; break;
                case (int)WIND.LIGHT:
                    wind = WIND.LIGHT; break;
                case (int)WIND.MEDIUM:
                    wind = WIND.MEDIUM; break;
                case (int)WIND.STRONG:
                    wind = WIND.STRONG; break;
                default:
                    wind = WIND.UNKNOWN; break;
            }
            PrintChange(MENU.WIND);
        }

        // 바람 상태 받기
        public string GetWind()
        {
            switch (wind)
            {
                case WIND.LEAST: return "LEAST";
                case WIND.LIGHT: return "LIGHT";
                case WIND.BREEZE: return "BREEZE";
                case WIND.MEDIUM: return "MIDEUM";
                case WIND.STRONG: return "STRONG";
                case WIND.UNKNOWN: return "UNKNOWN";
            }
            return "OFF";
        }

        // 전체 상태 출력
        public void PrintState()
        {
            Console.WriteLine($"[ PRINT AC {GetName()} | MODEL {GetModel()} ]");
            Console.WriteLine($"[ INFO ] POWER : {GetPower()} ");
            Console.WriteLine($"[ INFO ] MODE : {GetMode()} ");
            Console.WriteLine($"[ INFO ] WIND : {GetWind()} ");
        }

        // 상태 변경시 출력
        public void PrintChange(MENU menu)
        {
            if (menu == MENU.POWER)
                Console.WriteLine($"POWER : TURN {(IsOn ? "ON" : "OFF")}");
            else if (menu == MENU.WIND)
                Console.WriteLine($"WIND : {preWind} TO {wind}");
            else if (menu == MENU.MODE)
                Console.WriteLine($"MODE : {preMode} TO {mode}");
        }
    }
}