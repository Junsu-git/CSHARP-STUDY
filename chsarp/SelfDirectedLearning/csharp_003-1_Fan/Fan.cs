using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace csharp_003_1_Fan
{
    public class Fan
    {

        // 전원 상태 -> timer 가 0이 됐을 때 timer 상태를 자동으로 바꿈
        private System.Timers.Timer? aTimer;
        private string _name;
        private PWR_SPEED _speed;
        private PWR_STATUS _status;
        private PWR_SWING _swing;

        public PWR_STATUS Power
        {
            get => _status;
            set
            {
                _status = value;
                if (_status == PWR_STATUS.PWR_OFF)
                {
                    _speed = PWR_SPEED.SPD_LV_0;
                    _swing = PWR_SWING.SWING_OFF;
                }
            }
        }

        public PWR_SWING Swing
        {
            get => _swing;
            set => _swing = value;
        }
        public PWR_SPEED Speed
        {
            get => _speed;
            set => _speed = value;
        }
        public enum PWR_STATUS { PWR_ON, PWR_OFF };
        public enum PWR_SPEED { SPD_LV_0, SPD_LV_1, SPD_LV_2, SPD_LV_3, SPD_LV_4 };
        public enum PWR_SWING { SWING_ON, SWING_OFF };

        public Fan(string name, float time)
        {
            SetTimer(time);
            _name = name;
            _speed = 0;
            _status = PWR_STATUS.PWR_OFF;
            _speed = PWR_SPEED.SPD_LV_0;
            _swing = PWR_SWING.SWING_OFF;
        }
       
        public void controlRotate(PWR_SWING status)
        {
            if (!isPowerOn()) return ;
            _swing = status;
            //if (status == PWR_SWING.SWING_OFF) Console.Write($"[ INFO ] Fan {_name} Swing OFF\n");
            //else if (status == PWR_SWING.SWING_ON) Console.Write($"[ INFO ] Fan {_name} Swing ON\n");

        }

        private bool isPowerOn()
        {
            if (_status != PWR_STATUS.PWR_ON)
            {
                //Console.Write("[ WARN ] Fan is PowerDown before operating \n");
                return false;
            }

            return true;
        }

        public void controlSpeed(PWR_SPEED speed)
        {
            if (!isPowerOn()) return;
            switch (speed)
            {
                case PWR_SPEED.SPD_LV_0: 
                    //Console.Write($"[ INFO ] \t Fan {_name} Speed 0\n"); 
                    control_motor_driver(PWR_SPEED.SPD_LV_0); break;
                case PWR_SPEED.SPD_LV_1: 
                    //Console.Write($"[ INFO ] \t Fan {_name} Speed 1\n"); 
                    control_motor_driver(PWR_SPEED.SPD_LV_1); break;
                case PWR_SPEED.SPD_LV_2: 
                    //Console.Write($"[ INFO ] \t Fan {_name} Speed 2\n"); 
                    control_motor_driver(PWR_SPEED.SPD_LV_2); break;
                case PWR_SPEED.SPD_LV_3: 
                    //Console.Write($"[ INFO ] \t Fan {_name} Speed 3\n"); 
                    control_motor_driver(PWR_SPEED.SPD_LV_3); break;
                case PWR_SPEED.SPD_LV_4: 
                    //Console.Write($"[INFO] \t Fan {_name}  Speed 4\n"); 
                    control_motor_driver(PWR_SPEED.SPD_LV_4); break;
                default:
                    Console.Write("[ WARN ] \tFan Speed is None \n");
                    break;
            }
        }
        private void control_motor_driver(PWR_SPEED speed)
        {
            _speed = speed;
            //Console.Write($"[ INFO ] \t\t Fan {_name} Speed {_speed}\n");
        }


        // timer 기능을 추가해보자
        // 셋 타이머가 받아 왔을때. fan 객체를 생성하고 해당 fan 의 객체를 set timer 했을 때
        // 그떄 실행 되어 와야 하는데

        // 불 형태로 받아 와서 타이머를 돌릴 수 있도록 도중에 바꿔주고. 바꿔준 후에, 타이머를 초기화 할 수 있도록
        private void SetTimer(float time)
        {
            aTimer = new System.Timers.Timer(time);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object? source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("선풍기 전원 꺼진 시간: {0:HH:mm:ss.fff}",
                          e.SignalTime);
            this.Power = Fan.PWR_STATUS.PWR_OFF;
            this.PrintFan();
        }
        // 통계 로그 (콘솔 -> 파일)
        // file or streamreader() or cw; 
        // 로깅에 들어가면 좋은 것 , 현실 시간을 받아와서, 언제 어떤 기능이 바꼈는지, 몇번 부터 몇번을 생성 했는지,
        // 

        public void PrintFan()
        {
            Console.WriteLine($"{_name}의 선풍기 상태 출력");
            //Console.WriteLine($"{_fan.Index + 1}번째 선풍기의 상태\n");
            Console.WriteLine($"전원 상태: {(this._status == Fan.PWR_STATUS.PWR_ON ? "켜짐" : "꺼짐")}");
            Console.WriteLine($"바람 세기: {this._speed switch
            {
                Fan.PWR_SPEED.SPD_LV_0 => "바람 없음",
                Fan.PWR_SPEED.SPD_LV_1 => "미풍",
                Fan.PWR_SPEED.SPD_LV_2 => "약풍",
                Fan.PWR_SPEED.SPD_LV_3 => "강풍",
                Fan.PWR_SPEED.SPD_LV_4 => "초강풍",
                _ => "알 수 없음"
            }}");
            Console.WriteLine($"회전 상태: {(this._swing == Fan.PWR_SWING.SWING_ON ? "켜짐" : "꺼짐")}");
            
        }
        #region  Power Method list
        public void PowerOnOff(PWR_STATUS status)
        {
            if (status == PWR_STATUS.PWR_OFF) Console.Write($"[ INFO ] Fan {_name} Power OFF\n");
            else if (status == PWR_STATUS.PWR_ON) Console.Write($"[ INFO ] Fan {_name} Power ON\n");
        }

        public void PowerOn()
        {
            //PowerOnOff(PWR_STATUS.PWR_ON);
            this._status = Fan.PWR_STATUS.PWR_ON;
        }

        public void PowerOff()
        {
            //PowerOnOff(PWR_STATUS.PWR_OFF);
            this._status = Fan.PWR_STATUS.PWR_ON;
            PrintFan();
        }

        #endregion
    }
}
