using System.Net.NetworkInformation;
using System.Timers;
using System.Xml.Linq;

namespace csharp_004_Fan
{
    public class smartFan : Fan
    {
        private System.Timers.Timer autoOffTimer; // 자동 꺼짐 타이머 
        private double? _currentTemperature;
        private double? _targetTemperature;
        private System.Timers.Timer tempMonitorTimer; // 온도 모니터 타이머
        private string _log_prifx = "[ INFO-DERIVED ] ";

        public smartFan(double currentTemp)
        {
            init(currentTemp);
        }
        private void init(double? currentTemp, int interval = 1000)
        {
            _currentTemperature = currentTemp;
            configAutoOffTimer();
            configTempMonitorTimer(interval);
            tempMonitorTimer.Start(); // 감지만 함
        }

        // 자동화 타이머
        public void SetAutoOffTimer(int millisec)
        {
            autoOffTimer.Interval = millisec;

            Console.WriteLine($"{_log_prifx}AutoOff Start :: {DateTime.Now:HH:mm:ss.fff}");
            autoOffTimer.Start();
            //autoOffTimer.Enabled = true;
        }
        private void configAutoOffTimer()
        {
            autoOffTimer = new System.Timers.Timer();
            autoOffTimer.Elapsed += OnAutoOffEvent;
            autoOffTimer.AutoReset = false;
        }
        private void OnAutoOffEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine($"{_log_prifx}AutoOff Event :: {e.SignalTime:HH:mm:ss.fff}");
            PowerOff();
        }
        private void configTempMonitorTimer(double interval)
        {
            tempMonitorTimer = new System.Timers.Timer(interval);
            tempMonitorTimer.AutoReset = true;
            tempMonitorTimer.Elapsed += OnTempMonitorEvent;
        }

        private void OnTempMonitorEvent(object? sender, ElapsedEventArgs e)
        {
            if (isValidTempValue())
            {
                if (_currentTemperature >= _targetTemperature)
                {
                    if (!isPowerOn()) PowerOn();
                    else return;
                }
                else
                {
                    if (isPowerOn()) PowerOff();
                    else return;
                }
            }
        }

        private bool isValidTempValue()
        {
            bool v1 = _currentTemperature.HasValue;
            bool v2 = _targetTemperature.HasValue;
            if (v1 == true && v2 == true) { return true; }
            else return false;
        }

        public void SetCurrentTemperature(int currentTemperature)
        {
            _currentTemperature = currentTemperature;
        }

        public void SetTargetTemp(int refTemperature)
        {
            _targetTemperature = refTemperature;
        }

        public void ChangeTemp(int v)
        {
            _currentTemperature = v;
        }

        public void PowerOn(int interval)
        {
            Console.WriteLine($"{_log_prifx}PowerOn OVERLOADING");
            SetAutoOffTimer(interval);
            PowerOn();
        }
    }
}