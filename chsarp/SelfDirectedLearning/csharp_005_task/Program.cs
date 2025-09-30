using System.ComponentModel;
using System.Runtime.InteropServices;

namespace csharp_005_task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Circulator> cirList = new List<Circulator>();
            //circulator.SetPower(Circulator.POWER_STATUS.On);
            //circulator.SetWindSpeed(3);
            //circulator.SetTimer(5); // 5초 타이머 설정 (5초 후에 자동으로 꺼지도록)
            //circulator.SetWindMode(Circulator.WIND_MODE.NATURAL);
            //circulator.SetSwing(Circulator.SWING_STATUS.Off);
            //circulator.DisplayStatus();

            // 리드 라인을 통한 컴파일러 블로킹 설정
            Console.ReadLine();

            // 기능 목록
            // 1. 전원 켜기/끄기
            // 2. 풍량 조절 (1~5)
            // 3. 타이머 설정 (1~8시간)
            // 4. 바람 모드 설정 (일반, 수면, 자연)
            // 5. 현재 상태 표시

            AirConditioner ac = new AirConditioner();
            // 기능 목록
            // 1. 전원 켜기/끄기
            // 2. 온도 설정 (16~30도)
            // 3. 풍량 조절 (1~5)
            // 4. 모드 설정 (냉방, 난방, 제습, 송풍)
            

        }


    }
}
