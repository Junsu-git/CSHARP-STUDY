namespace csharp_003_1_Fan
{
    internal class Program
    {       
        static void Main(string[] args)
        {
            Fan fan = new Fan("FAN_1");
            Fan fan2 = new Fan("FAN_2");
            
            fan.PowerOnOff(Fan.PWR_STATUS.PWR_ON);
            fan.PowerOn();
            fan.PowerOff();

            fan2.PowerOnOff(Fan.PWR_STATUS.PWR_ON);
            fan.controlSpeed(Fan.PWR_SPEED.SPD_LV_1);
            fan.controlRotate(Fan.PWR_SWING.SWING_ON);
            fan2.controlSpeed(Fan.PWR_SPEED.SPD_LV_1);
            fan2.controlRotate(Fan.PWR_SWING.SWING_ON);
            fan.PowerOnOff(Fan.PWR_STATUS.PWR_OFF);
            fan2.PowerOnOff(Fan.PWR_STATUS.PWR_OFF);
        }
    }

   
}
