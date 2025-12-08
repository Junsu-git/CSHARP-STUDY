namespace TowerDefense.Core
{
    public class NormalEnemy : Enemy
    {
        public override EnemyType Type => EnemyType.Normal;
        public NormalEnemy()
        {
            BaseHealth = 80;
            BaseAttackPower = 5;
            BaseRange = 1;
            BonusHealth = 10;
            BehaviorPattern = "Straight Line";
        }
    }
}