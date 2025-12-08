namespace TowerDefense.Core
{
    public class RangedTower : Tower
    {
        public override TowerType Type => TowerType.Ranged;
        public RangedTower()
        {
            BaseHealth = 120;
            BaseAttackPower = 8;
            BaseRange = 15; // 장거리
            BonusHealth = 10;
        }
    }
}