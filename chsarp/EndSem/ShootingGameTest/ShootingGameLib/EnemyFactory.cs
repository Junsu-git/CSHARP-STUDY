namespace ShootingGameLib
{
    public static class EnemyFactory
    {
        private static int _idCounter = 1;

        // [신규] ID 카운터 초기화 메서드 추가
        public static void ResetIdCounter()
        {
            _idCounter = 1;
        }

        public static Enemy CreateEnemy(EnemyType type, int order, int x, int y)
        {
            Enemy ? enemy = null;
            switch (type)
            {
                case EnemyType.Normal:
                    enemy = new Enemy("잡몹", 50, 5, type) { Pattern = "직진 하강" };
                    break;
                case EnemyType.MidBoss:
                    enemy = new Enemy("중보", 200, 15, type) { Pattern = "좌우 왕복, 공격" };
                    break;
                case EnemyType.FinalBoss:
                    enemy = new Enemy("막보", 1000, 50, type) { Pattern = "순간 돌진, 총알 난사" };
                    break;
            }

            if (enemy != null)
            {
                enemy.SpawnId = _idCounter++;
                enemy.SpawnOrder = order;
                enemy.X = x;
                enemy.Y = y;
            }
            return enemy;
        }
    }
}