namespace reviewLib.Fan
{
    class Fan
    {
        // fields for Fan
        POWER_STATE power;
        SWING_STATE swing;
        SPEED_STATE speed;

        // Properties that considered capsuliizing
        public int Index { get; }

        public POWER_STATE Power
        {
            get => power;
            set
            {
                power = value;
                if (power == POWER_STATE.POWER_OFF)
                {
                    Speed = SPEED_STATE.SPEED_LV0;
                    Swing = SWING_STATE.SWING_OFF;
                }
            }
        }
        public SWING_STATE Swing
        {
            get => swing;
            set => swing = value;
        }
        public SPEED_STATE Speed
        {
            get => speed;
            set => speed = value;
        }
        // defined enum variables
        public enum POWER_STATE { POWER_ON, POWER_OFF }
        public enum SWING_STATE { SWING_ON, SWING_OFF }
        // POWER_STATE is POWER_ON and SPEED_STATE is SPEED_LV0
        // It means that the power is on but motor is not rotating
        public enum SPEED_STATE { SPEED_LV0, SPEED_LV1, SPEED_LV2, SPEED_LV3 }

        public Fan(int _index)
        {
            Index = _index;
            Power = POWER_STATE.POWER_OFF;
            Swing = SWING_STATE.SWING_OFF;
            Speed = SPEED_STATE.SPEED_LV0;
        }

        public void Apply(Fan _fan)
        {
            power = _fan.Power;
            Swing = _fan.Swing;
            Speed = _fan.Speed;
        }
    }
}