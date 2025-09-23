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
        //private System.Timers.Timer? aTimer;

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

        public Fan()
        {
            Init();
        }
        public Fan(string name)
        {
            Init(name);
        }

        // 이닛 할 때
        private void Init(string name = "DEFAULT")
        {
            _name = name;
            _speed = 0;
            _status = PWR_STATUS.PWR_OFF;
            _speed = PWR_SPEED.SPD_LV_0;
            _swing = PWR_SWING.SWING_OFF;
        }
        public string GetFanName()
        {
            return _name;
        }

        // 회전 여부 변경
        public void ControlRotate(PWR_SWING status)
        {
            if (!isPowerOn()) return;
            _swing = status;
            Console.WriteLine($"[ INFO ] FAN_{_name} SWING {(this._swing == Fan.PWR_SWING.SWING_ON ? "ON" : "OFF")}");
        }

        // 전원 켜짐 여부 예외처리
        private bool isPowerOn()
        {
            if (_status != PWR_STATUS.PWR_ON)
            {
                return false;
            }
            return true;
        }

        public void ControlSpeed(PWR_SPEED speed)
        {
            if (!isPowerOn()) return;
            switch (speed)
            {
                case PWR_SPEED.SPD_LV_0:
                    //Console.Write($"[ INFO ] \t Fan {_name} Speed 0\n"); 
                    ChangeFanSpeed(PWR_SPEED.SPD_LV_0); break;
                case PWR_SPEED.SPD_LV_1:
                    //Console.Write($"[ INFO ] \t Fan {_name} Speed 1\n"); 
                    ChangeFanSpeed(PWR_SPEED.SPD_LV_1); break;
                case PWR_SPEED.SPD_LV_2: 
                    //Console.Write($"[ INFO ] \t Fan {_name} Speed 2\n"); 
                    ChangeFanSpeed(PWR_SPEED.SPD_LV_2); break;
                case PWR_SPEED.SPD_LV_3: 
                    //Console.Write($"[ INFO ] \t Fan {_name} Speed 3\n"); 
                    ChangeFanSpeed(PWR_SPEED.SPD_LV_3); break;
                case PWR_SPEED.SPD_LV_4: 
                    //Console.Write($"[INFO] \t Fan {_name}  Speed 4\n"); 
                    ChangeFanSpeed(PWR_SPEED.SPD_LV_4); break;
                default:
                    Console.Write("[ WARN ] \tFan Speed is None \n");
                    break;
            }
        }
        private void ChangeFanSpeed(PWR_SPEED speed)
        {
            Speed = speed;
            Console.WriteLine($"[ INFO ] FAN_{_name} SPEED_LEVEL CHANGED TO {Speed switch
            {
                Fan.PWR_SPEED.SPD_LV_0 => "LV0",
                Fan.PWR_SPEED.SPD_LV_1 => "LV1",
                Fan.PWR_SPEED.SPD_LV_2 => "LV2",
                Fan.PWR_SPEED.SPD_LV_3 => "LV3",
                Fan.PWR_SPEED.SPD_LV_4 => "LV4",
                _ => "UNKNOWN"
            }}");
        }
        public void PrintFan()
        {
            Console.WriteLine($"[ INFO ] PRINT FAN_{_name}\n");
            Console.WriteLine($"POWER_STATE = {(this._status == Fan.PWR_STATUS.PWR_ON ? "ON" : "OFF")}");
            Console.WriteLine($"\tSPEED_LEVEL = {this._speed switch
            {
                Fan.PWR_SPEED.SPD_LV_0 => "LV0",
                Fan.PWR_SPEED.SPD_LV_1 => "LV1",
                Fan.PWR_SPEED.SPD_LV_2 => "LV2",
                Fan.PWR_SPEED.SPD_LV_3 => "LV3",
                Fan.PWR_SPEED.SPD_LV_4 => "LV4",
                _ => "UNKNOWN"
            }}");
            Console.WriteLine($"\t\tROTATE_STATE = {(this._swing == Fan.PWR_SWING.SWING_ON ? "ON" : "OFF")}");
        }

        public void PowerOn()
        {
            if (isPowerOn()) return;
            this._status = Fan.PWR_STATUS.PWR_ON;
            Console.WriteLine($"[ INFO ] FAN_{_name} TURN ON");
        }

        public void PowerOff()
        {
            if (!isPowerOn()) return;
            Power = PWR_STATUS.PWR_OFF;
            Console.WriteLine($"[ INFO ] FAN_{_name} TURN OFF\n");
            PrintFan();
        }
    }
}
