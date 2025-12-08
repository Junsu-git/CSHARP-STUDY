namespace TowerDefense.Core
{
    public class FinalBossEnemy : Enemy
    {
        public override EnemyType Type => EnemyType.FinalBoss;
        public FinalBossEnemy()
        {
            BaseHealth = 800;
            BaseAttackPower = 30;
            BaseRange = 10;
            BonusHealth = 100;
            BonusAttackPower = 10;
            BehaviorPattern = "Complex Multi-Stage";
        }
    }
}