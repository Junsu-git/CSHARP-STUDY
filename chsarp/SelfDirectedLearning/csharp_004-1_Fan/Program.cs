namespace csharp_004_Fan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Fan 클래스 설계
            // 캡슐화로 내부 데이터와 기능을 결합(Encapsulation)            
            // 핵심 기능과 속성만 추출하여 설계  (Abstraction)            
            //Fan fan = new Fan(); // 기본 클래스 기능 추가

            // 상속을 통한 기능 추가와 다형성 (inheritance, polymorphism)
            // 생성자 오버로딩으로 다형성 (polymorphism)
            smartFan smartfan = new smartFan(22); 
            
            // 꺼짐 예약 타이머 기능 추가
            // autoOffTimer
                //smartfan.PowerOn();
                //smartfan.SetAutoOffTimer(1000);
                // override testing
                smartfan.PowerOn(2500);
                Console.WriteLine("Testing autoOffTimer .... wait powerOff Message...");
                Console.ReadLine();

            // 온도설정에 따른 자동 OnOff 기능 추가
            // Themometer
                // 현재온도는 기본설정을 생성자 상단 참조
                // 현재온도는 메소드로 설정
                smartfan.SetCurrentTemperature(22);
                // 온도에 따른 자동 OnOff 동작을 위한 기준 온도 설정
                smartfan.SetTargetTemp(25);
                smartfan.ChangeTemp(26); // 시뮬레이션을 위한 강제 동작 구현
                Console.WriteLine("Testing TempMonitorTimer .... wait powerOn Message...");
                Console.ReadLine();
                smartfan.ChangeTemp(21); // 시뮬레이션을 위한 강제 동작 구현
                Console.WriteLine("Testing TempMonitorTimer .... wait powerOff Message...");
            Console.ReadLine();

        }
    }
}
