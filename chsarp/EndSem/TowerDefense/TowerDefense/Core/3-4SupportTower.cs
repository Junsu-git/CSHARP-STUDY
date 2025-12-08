namespace TowerDefense.Core
{
    public class SupportTower : Tower
    {
        public override TowerType Type => TowerType.Support;
        public SupportTower()
        {
            BaseHealth = 100;
            BaseAttackPower = 1; // 낮은 공격력
            BaseRange = 10;
            BonusAttackPower = 0;
            BonusHealth = 50;
        }
    }
}