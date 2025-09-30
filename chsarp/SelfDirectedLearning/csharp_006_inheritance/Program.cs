using System.Net.Http.Headers;

namespace csharp_006_inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 메인 역할 = 시뮬레이터
            // 제품에 리모컨 제작 / 개별로
            // 만능 리모컨 설계
            Fan fan = new Fan();
            Circulator circulator = new Circulator();
            AirConditioner airConditioner = new AirConditioner();


            // 수동적으로 넣어서 켜줌 (상속으로 인한 다형성을 사용해서, 다만 불편)
            //PowerControl pctrl = fan; pctrl.power_on();
            //pctrl = circulator; pctrl.power_on();
            //pctrl = airConditioner; pctrl.power_on();

            // 리스트 안에 담은 뒤 반복문으로 켜줌

            // 리모컨으로 제어하기
            // 상위 만능 리모컨 만들기
            // 공장에서 만들기

            List<PowerControl> list = new List<PowerControl>();
            list.Add(fan);
            list.Add(airConditioner);
            list.Add(circulator);

            foreach (var obj in list) { obj.power_on(); }

        }
        //static void power_on_all_device()
        //{
        //    Fan fan = new Fan();
        //    Circulator circulator = new Circulator();
        //    AirConditioner airConditioner = new AirConditioner();

        //    fan.turnOn();
        //    circulator.PWR_ON();
        //    airConditioner.power_on();
        //}
    }
    public class PowerControl()
    {
        public virtual void power_on() { Console.WriteLine("Device power on"); }
    }
    public class Fan : PowerControl 
    {
        public override void power_on() { turnOn(); }
        public void turnOn() { Console.WriteLine("Fan power on"); }
        public void control_speed(int speed) { Console.WriteLine($"Fan control speed {speed}"); }
        public void set_rotate() { Console.WriteLine($"Fan control rotate default"); }
        public void set_rotate(bool dir) { Console.WriteLine($"Fan control rotate {dir}"); }
    }
    public class Circulator : PowerControl
    {
        public override void power_on() { PWR_ON(); }
        public void PWR_ON() { Console.WriteLine("Circulator power on"); }
        public void control_speed(int speed) { Console.WriteLine($"Circulator control speed {speed}"); }
        public void control_speed(double speed) { Console.WriteLine($"Circulator speed {speed}"); }
        public void set_swing() { Console.WriteLine($"Circulator swing default"); }
    }
    public class AirConditioner : PowerControl
    {
        public override void power_on() { power_On(); }
        public void power_On() { Console.WriteLine("AirConditioner power on"); }
        public void control_speed(int speed) { Console.WriteLine($"AirConditioner control speed {speed}"); }
        public void control_speed(double speed) { Console.WriteLine($"AirConditioner speed {speed}"); }
        public void set_direction() { Console.WriteLine($"AirConditioner direction default"); }
    }

}
