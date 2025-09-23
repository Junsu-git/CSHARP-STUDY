using System.Net.Http.Headers;

namespace csharp_003_1_Fan
{
    internal class Program
    {       
        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            List<Fan> fans = new List<Fan>();
            AddFans(fans, 1000);

            PowerOnFans(fans);
            //SmartFan smartFan1 = new SmartFan();
            //SmartFan smartFan2 = new SmartFan();

            //smartFan1.PowerOn(3000);
            //smartFan1.ControlRotate(Fan.PWR_SWING.SWING_ON);
            //Console.ReadLine();

            //smartFan2.PowerOn(11.0);
            //smartFan2.ControlSpeed(Fan.PWR_SPEED.SPD_LV_3);

            //Console.ReadLine();
        }

        private static void PowerOnFans(List<Fan> fans)
        {
            foreach (var fan in fans)
            {
                fan.PowerOn();
            }
        }

        private static void AddFans(List<Fan> fans, int counts)
        {
            for (int i = 0; i < counts; i++)
            {
                fans.Add(new Fan($"{i.ToString()}"));
            }
        }
        
    }
}
