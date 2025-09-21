namespace csharp_003_1_Fan
{
    internal class TempSystem
    {
        double maxT = 20;   // 30도 되면 켜짐
        double minT = 0;    // 0도보다 낮아지면 꺼짐
        double curT;
        bool isReady = false;

        // 현재 온도 설정
        public void SetCurTemp(double temp)
        {
            curT = temp;
        }

        // 춥니?
        public bool isCold()
        {
            if (minT >= curT) return true;
            return false;
        }
        
        // 덥니?
        public bool isHot()
        {
            if (maxT <= curT) return true;
            return false;
        }
        
        // 온도 변화 해버리기~
        public void ChangeCurTemp(double power)
        {
            curT += power;
        }

        public double GetMaxT()
        {
            return maxT;
        }
        public double GetMinT()
        {
            return minT;
        }
        public double GetCurT()
        {
            return curT;
        }
    }
}