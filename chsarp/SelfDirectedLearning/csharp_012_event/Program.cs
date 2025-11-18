using System.Threading.Channels;

namespace csharp_012_event
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var myTimer = new System.Timers.Timer();
            myTimer.Elapsed += (sender, e) => { Console.WriteLine("Events TST with ramda"); };

            myTimer.Interval = 1000;
            myTimer.Start();

            Console.ReadLine();

            // 온도 객체를 하나 만든다
            Temperature temp = new Temperature()
            {
                MaxTemperature = 80.0,
                MinTemperature = 10.0
            };

            
        }
    }
    class Temperature
    {
        private float currentT;
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        
        public event EventHandler
        public float GetTemperature()
        {
            return currentT;
        }
        public bool IsExceedMaxTemp()
        {
            return false;
        }
        
    }

    public class TemperatueEventArgs
    {
        public double Temperature { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
