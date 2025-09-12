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
        private string _name;
        private int _speed;
        private PWR_STATUS _status;

        public enum PWR_STATUS { PWR_ON, PWR_OFF };
        public enum PWR_SPEED { SPD_LV_0, SPD_LV_1, SPD_LV_2, SPD_LV_3, SPD_LV_4 };
        public enum PWR_SWING { SWING_ON, SWING_OFF };
        public Fan(string name)
        {
            _name = name;
            _speed = 0;
            _status = PWR_STATUS.PWR_OFF;
        }
       
        public void controlRotate(PWR_SWING status)
        {
            if (isPowerOn()) return;
            if (status == PWR_SWING.SWING_OFF) Console.Write($"[ INFO ] Fan {_name} Swing OFF\n");
            else if (status == PWR_SWING.SWING_ON) Console.Write($"[ INFO ] Fan {_name} Swing ON\n");
        }

        private bool isPowerOn()
        {
            if (_status != PWR_STATUS.PWR_ON)
            {
                Console.Write("[ WARN ] Fan is PowerDown before operating \n");
            }

            return true;
        }

        public void controlSpeed(PWR_SPEED speed)
        {
            isPowerOn();
            switch (speed)
            {
                case PWR_SPEED.SPD_LV_0: Console.Write($"[ INFO ] \t Fan {_name} Speed 0\n"); control_motor_driver(0); break;
                case PWR_SPEED.SPD_LV_1: Console.Write($"[ INFO ] \t Fan {_name} Speed 1\n"); control_motor_driver(1); break;
                case PWR_SPEED.SPD_LV_2: Console.Write($"[ INFO ] \t Fan {_name} Speed 2\n"); control_motor_driver(2); break;
                case PWR_SPEED.SPD_LV_3: Console.Write($"[ INFO ] \t Fan {_name} Speed 3\n"); control_motor_driver(3); break;
                case PWR_SPEED.SPD_LV_4: Console.Write($"[INFO] \t Fan {_name}  Speed 4\n"); control_motor_driver(4); break;
                default:
                    Console.Write("[ WARN ] \tFan Speed is None \n");
                    break;
            }
        }
        private void control_motor_driver(int speed)
        {
            _speed = speed;
            Console.Write($"[ INFO ] \t\t Fan {_name} Speed {_speed}\n");
        }


        #region  Power Method list
        public void PowerOnOff(PWR_STATUS status)
        {
            if (status == PWR_STATUS.PWR_OFF) Console.Write($"[ INFO ] Fan {_name} Power OFF\n");
            else if (status == PWR_STATUS.PWR_ON) Console.Write($"[ INFO ] Fan {_name} Power ON\n");
        }

        public void PowerOn()
        {
            PowerOnOff(PWR_STATUS.PWR_ON);   
        }

        public void PowerOff()
        {
            PowerOnOff(PWR_STATUS.PWR_OFF);
        }
        #endregion
    }
}
