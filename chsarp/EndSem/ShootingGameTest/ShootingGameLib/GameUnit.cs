namespace ShootingGameLib
{
    public abstract class GameUnit
    {
        public string Name { get; set; }
        public int Level { get; protected set; } = 1;
        public int BaseHp { get; set; }
        public int BaseAttack { get; set; }
        public int BonusHp { get; set; }
        public int BonusAttack { get; set; }
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public int CurrentHp => BaseHp + (Level * 10) + BonusHp;
        public int CurrentAttack => BaseAttack + (Level * 2) + BonusAttack;

        public GameUnit(string name, int hp, int atk)
        {
            Name = name;
            BaseHp = hp;
            BaseAttack = atk;
        }

        public void SetLevel(int newLevel) => Level = newLevel;
        public abstract void Action();
    }
}