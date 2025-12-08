namespace TowerDefense.Core
{
    public class MeleeTower : Tower
    {
        public override TowerType Type => TowerType.Melee;
        public MeleeTower()
        {
            BaseHealth = 150;
            BaseAttackPower = 15;
            BaseRange = 3; // 근거리
            BonusAttackPower = 5;
        }
    }
}