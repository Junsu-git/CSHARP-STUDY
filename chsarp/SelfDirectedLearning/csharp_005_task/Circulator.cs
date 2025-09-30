using System;

namespace csharp_005_task
{
    internal class Circulator
    {
        private string? name;
        private System.Timers.Timer? timer; // 타이머
        private bool isActive;              // 실제 활동 여부
        private bool isSwing;               // 회전 여부
        private POWER power;                // 전원 상태
        private SPEED speed;                // 바람 세기

        // 절전 모드 들어가기 전의 상태를 기억
        private SPEED prevSpeed;
        private bool prevSwing;

        private enum POWER { PWR_ON, PWR_OFF, PWR_ECO }
        private enum SPEED { SPD_LV0, SPD_LV1, SPD_LV2, SPD_LV3 }

        // ---- 생성자 ----
        public Circulator(string name = "Default", int timerSeconds = 0)
        {
            Init(name, timerSeconds);
        }

        private void Init(string name, int timerSeconds)
        {
            this.name = name;
            this.power = POWER.PWR_OFF;
            this.speed = SPEED.SPD_LV0;
            this.isSwing = false;
            this.isActive = false;

            this.prevSpeed = SPEED.SPD_LV1; // 기본 저장값
            this.prevSwing = false;

            if (timerSeconds > 0)
            {
                timer = new System.Timers.Timer(timerSeconds * 1000);
                timer.Elapsed += (s, e) => PowerOff();
                timer.AutoReset = false;
            }
        }

        // ---- 기능 메서드 ----
        public void PowerOn()
        {
            if (power == POWER.PWR_ECO)
            {
                // 에코모드 복귀 → 이전 상태 복원
                speed = prevSpeed;
                isSwing = prevSwing;
                isActive = true;
            }
            else
            {
                // 일반 전원 켜기
                speed = SPEED.SPD_LV1;
                isSwing = false;
                isActive = true;
            }

            power = POWER.PWR_ON;
            if (timer != null) timer.Start();
        }

        public void PowerOff()
        {
            power = POWER.PWR_OFF;
            speed = SPEED.SPD_LV0;
            isActive = false;
            isSwing = false;
            if (timer != null) timer.Stop();
        }

        public void PowerSetEco()
        {
            // 현재 상태 저장
            prevSpeed = speed;
            prevSwing = isSwing;

            // 절전 모드 전환
            power = POWER.PWR_ECO;
            speed = SPEED.SPD_LV0;
            isSwing = false;
            isActive = true;

            if (timer != null) timer.Start();
        }

        public void SetSpeed(int level)
        {
            if (power == POWER.PWR_OFF || power == POWER.PWR_ECO) return;

            switch (level)
            {
                case 0: speed = SPEED.SPD_LV0; break;
                case 1: speed = SPEED.SPD_LV1; break;
                case 2: speed = SPEED.SPD_LV2; break;
                case 3: speed = SPEED.SPD_LV3; break;
                default: throw new ArgumentException("잘못된 속도 레벨입니다.");
            }
        }

        public void ToggleSwing()
        {
            if (power == POWER.PWR_OFF || power == POWER.PWR_ECO) return;
            isSwing = !isSwing;
        }

        // ---- 상태 반환 메서드들 ----
        public string GetActive()
        {
            return isActive ? "ACTIVE" : "INACTIVE";
        }

        public string GetSwing()
        {
            if (!IsPowerOn()) return "NO POWER";
            return isSwing ? "SWING" : "NOT SWING";
        }

        public string GetSpeed()
        {
            if (!IsPowerOn()) return "NO POWER";
            return speed.ToString();
        }

        public string GetPower()
        {
            return power.ToString();
        }

        public bool IsPowerOn()
        {
            return power == POWER.PWR_ON;
        }

        // ---- 최종 상태 반환 ----
        public string GetStatus()
        {
            return $"[{name}] " +
                   $"Power={GetPower()}, " +
                   $"Active={GetActive()}, " +
                   $"Speed={GetSpeed()}, " +
                   $"Swing={GetSwing()}";
        }
    }
}
