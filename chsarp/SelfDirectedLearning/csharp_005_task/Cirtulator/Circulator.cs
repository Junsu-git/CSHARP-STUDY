using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace csharp_005_task.Cirtulator
{
    internal class Circulator
    {
        // 전원 필드
        POWER_STATE power;

        // 속도 필드
        WIND_SPEED speed;

        // 전원 프로퍼티
        POWER_STATE Power { 
            get => power; 
            set
            {
                power = value;
                if (power == POWER_STATE.POWER_OFF)
                {
                    isSwing = false;
                    Speed = WIND_SPEED.SPEED_LV0;
                    isWorking = false;
                }
            }
        }
        // 스피드 프로퍼티
        WIND_SPEED Speed { get => speed; set => speed = value; }
        System.Timers.Timer autoOffTimer;


        enum POWER_STATE { POWER_ON, POWER_OFF, POWER_ECO}
        enum WIND_SPEED { SPEED_LV0, SPEED_LV1, SPEED_LV2, SPEED_LV3, SPEED_LV4 }
        bool isSwing;
        bool isWorking;

        // 필드 값에 따라 문자열을 반환 해주는 함수 : 전원 모드
        private string GetPowerState(POWER_STATE ps)
        {
            switch (ps)
            {
                case POWER_STATE.POWER_ON:
                    return "ON ";
                case POWER_STATE.POWER_OFF:
                    return "OFF";
                case POWER_STATE.POWER_ECO:
                    return "ECO";
            }
            throw new ArgumentException("Invalid POWER_STATE value");
        } 
        
        // 필드 값에 따라 문자열을 반환 해주는 함수 : 전원 모드
        private string GetSpeedState(WIND_SPEED ws)
        {
            switch (ws)
            {
                case WIND_SPEED.SPEED_LV0:
                    return "SPEED LV 0";
                case POWER_STATE.POWER_OFF:
                    return "OFF";
                case POWER_STATE.POWER_ECO:
                    return "ECO";
            }
            throw new ArgumentException("Invalid POWER_STATE value");
        }
    }
}