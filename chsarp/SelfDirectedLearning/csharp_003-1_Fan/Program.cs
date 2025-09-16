namespace csharp_003_1_Fan
{
    internal class Program
    {       
        static void Main(string[] args)
        {
            SmartFan smFan = new SmartFan();
            Fan fan = new Fan("FAN_1", 4000);

            Console.WriteLine("선풍기 초기 상태 출력");
            Console.WriteLine();
            fan.PrintFan();
            Console.WriteLine();

            fan.PowerOn();
            fan.controlSpeed(Fan.PWR_SPEED.SPD_LV_1);
            fan.controlRotate(Fan.PWR_SWING.SWING_ON);

            Console.WriteLine("변경된 선풍기 상태 출력");
            fan.PrintFan();

            Console.WriteLine();

            // 입력 받은 후에 온도 값 체크해서 이제 해보자 
            Console.ReadLine();

            // 집 가서 온도 시스템 만들기
            //fan.SetCurrentTemprature(22);
            //fan.SetTargetTemp(25);
            //fan.ChangeTemp(26);
            //Console.WriteLine("Testing TempMonitor Timer ... wait powerOn Message...");
            //Console.ReadLine();
            //fan.ChangeTemp(21);
            //Console.WriteLine("Testing TempMonitor Timer ... wait powerOff Message...");
            //Console.ReadLine();
        }
    }

   
}
