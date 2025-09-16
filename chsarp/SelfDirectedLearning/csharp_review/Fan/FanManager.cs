namespace reviewLib.Fan
{
    internal class FanManager
    {
        List<Fan> list = new List<Fan>();
        int Index { get; set; }

        public FanManager() { Index = 0; }
        public List<Fan> GetList => list;
        public int GetListLength => list.Count;

        public void CreateFan() => list.Add(new Fan(Index++));

        public Fan GetFan(int index) => list[index];

        // 빈 리스트인가?
        public bool IsListEmpty()
        {
            return list.Count == 0;
        }

        // 입력한 값이 리스트의 길이보다 큰가?
        public bool IsListOverflow(int uInput)
        {
            return list.Count <= uInput;
        }

        public bool IsValidIndex(int uInput)
        {
            if (uInput >= 0 && !IsListOverflow(uInput)) return true;
            else return false;
        } 

        public void PrintFan(Fan _fan)
        {
            Console.WriteLine("==================================");
            Console.WriteLine($"{_fan.Index + 1}번째 선풍기의 상태\n");
            Console.WriteLine($"전원 상태: {(_fan.Power == Fan.POWER_STATE.POWER_ON ? "켜짐" : "꺼짐") }");
            Console.WriteLine($"바람 세기: {_fan.Speed switch
            {
                Fan.SPEED_STATE.SPEED_LV0 => "바람 없음",
                Fan.SPEED_STATE.SPEED_LV1 => "미풍",
                Fan.SPEED_STATE.SPEED_LV2 => "약풍",
                Fan.SPEED_STATE.SPEED_LV3 => "강풍",
                _ => "알 수 없음"
            }}");
            Console.WriteLine($"회전 상태: {(_fan.Swing == Fan.SWING_STATE.SWING_ON ? "켜짐" : "꺼짐")}");
            Console.WriteLine("==================================\n");
        }
        // 타이머 기능 추가
        // system.timers.timer
        // system.threading.timer


    }
}