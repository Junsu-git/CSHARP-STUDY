using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// AccessModifier
namespace Chap7
{
    public class WaterHeater
    {
        protected int temperature;

        public void SetTemperature(int temprature)
        {
            if (temperature < -5 || temprature > 42) throw new Exception("Out of temperature range");
            this.temperature = temprature;
        }
        internal void TurnOnWater()
        {
            Console.WriteLine($"Turn on water : {temperature}");
        }

        public static void Run()
        {
            try
            {
                WaterHeater heater = new WaterHeater();
                heater.SetTemperature(20);
                heater.TurnOnWater();

                heater.SetTemperature(-2);
                heater.TurnOnWater();

                heater.SetTemperature(50);
                heater.TurnOnWater();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("====================");
        }
    }
}
