using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Timers;

namespace csharp_003_1_Fan
{
    internal class SmartFan : Fan
    {
        TempSystem temp = new TempSystem(); // get set 을 통해 받아올 수 있음.
        System.Timers.Timer? autoOffTimer;     // 자동 켜기 끄기 타이머
        System.Timers.Timer? tempMonitorTimer;   // 온도 감지 타이머
        Fan prevFan;

        public SmartFan()
        {
            prevFan = new Fan("PREV");
            prevFan.PowerOn();
            ConfigAutoOffTimer();
            ConfigTempMonitorTimer(1000);
        }

        // 자동 꺼짐 타이머 기본 세팅
        private void ConfigAutoOffTimer()
        {
            autoOffTimer = new System.Timers.Timer();   // autoOffTimer에 새 객체 연결
            autoOffTimer.Elapsed += OnAutoOffEvent;     // 시간이 지났을 때 호출할 함수 연결
            autoOffTimer.AutoReset = false;             // 반복상태 off
        }

        // 타이머를 인자값으로 하는 스마트 팬 켜짐
        public void PowerOn(int interval)
        {
            Console.WriteLine($"[ INFO ] FAN_{GetFanName()} ACTIVE | MODE : AUTO OFF");
            SetAutoOffTimer(interval);  // 꺼짐 주기 설정
            PowerOn();
        }

        // 타이머에 따라 인터벌을 세팅하고, 타이머 실행
        private void SetAutoOffTimer(int millisec)
        {
            autoOffTimer.Interval = millisec;
            autoOffTimer.Start();
            //autoOffTimer.Enabled = true;
        }

        private void OnAutoOffEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine($"[ INFO ] FAN_{GetFanName()}  AutoOff Event :: {e.SignalTime:HH:mm:ss.fff}");
            PowerOff();
        }

        // 오버로딩 함수 2(온도를 인자값으로 받을 때)
        public void PowerOn(double curV)
        {
            Console.WriteLine($"[ INFO ] FAN_{GetFanName()} ACTIVE | MODE : TEMP_MONITOR");
            SetTempMonitor(curV);
            PowerOn();
        }
        private void SetTempMonitor(double curV)
        {
            temp.SetCurTemp(curV);
            tempMonitorTimer.Start();
        }
        private void ConfigTempMonitorTimer(double interval)
        {
            tempMonitorTimer = new System.Timers.Timer(interval);
            tempMonitorTimer.AutoReset = true;
            tempMonitorTimer.Elapsed += OnTempMonitorEvent;
        }

        private void OnTempMonitorEvent(object? sender, ElapsedEventArgs e)
        {
            ChangeTempBySpeed();
            ChangePowerByTemp();
        }

        private void ChangePowerByTemp()
        {
            if (temp.isCold())
            {
                prevFan.Speed = base.Speed;
                prevFan.Swing = base.Swing;

                Console.WriteLine("==============================");
                prevFan.PrintFan();
                Console.WriteLine("==============================");
                PowerOff();
            }
            else if (temp.isHot())
            {
                PowerOn();
                Console.WriteLine("==============================");
                prevFan.PrintFan();
                Console.WriteLine("==============================");
                base.Speed = prevFan.Speed;
                base.Swing = prevFan.Swing;
            }
        }

        private void ChangeTempBySpeed()
        {
            // 매 1초마다 선풍기 속도에 따른 tempSystem의 온도 변경
            switch (this.Speed)
            {
                case Fan.PWR_SPEED.SPD_LV_0:
                    ChangeTempAndTerminal(1);
                    break;
                case Fan.PWR_SPEED.SPD_LV_1:
                    ChangeTempAndTerminal(-1);
                    break;
                case Fan.PWR_SPEED.SPD_LV_2:
                    ChangeTempAndTerminal(-2);
                    break;
                case Fan.PWR_SPEED.SPD_LV_3:
                    ChangeTempAndTerminal(-3);
                    break;
                case Fan.PWR_SPEED.SPD_LV_4:
                    ChangeTempAndTerminal(-5);
                    break;
            }
        }

        private void ChangeTempAndTerminal(double power)
        {
            //Console.Clear();
            temp.ChangeCurTemp(power); // 온도 감소
            PrintFan();            // 상태 출력
        }

        // 오버로딩 함수 3(둘 다 인자값으로 받을 때)
        public void PowerOn(int interval, double curT)
        {
            PowerOn(interval);
            PowerOn(curT);
            PowerOn();
        }

        public void PrintFan()
        {
            base.PrintFan();
            Console.WriteLine($"\nMAX_TEMP = {temp.GetMaxT()} || MIN_TEMP = {temp.GetMinT()}");
            Console.WriteLine($"CUR_FAN_TEMP = {temp.GetCurT()}");
        }
    }
}