namespace csharp_003_1_Fan
{
    internal class Program
    {       
        static void Main(string[] args)
        {
            SmartFan smartFan1 = new SmartFan();
            SmartFan smartFan2 = new SmartFan();

            smartFan1.PowerOn(3000);
            smartFan1.ControlRotate(Fan.PWR_SWING.SWING_ON);
            Console.ReadLine();

            smartFan2.PowerOn(11.0);
            smartFan2.ControlSpeed(Fan.PWR_SPEED.SPD_LV_3);

            Console.ReadLine();
        }
    }
}
