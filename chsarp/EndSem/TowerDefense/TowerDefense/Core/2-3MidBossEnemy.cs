namespace TowerDefense.Core
{
    public class MidBossEnemy : Enemy
    {
        public override EnemyType Type => EnemyType.MidBoss;
        public MidBossEnemy()
        {
            BaseHealth = 250;
            BaseAttackPower = 15;
            BaseRange = 5;
            BonusHealth = 50;
            BehaviorPattern = "Sinusoidal Movement";
        }
    }
}