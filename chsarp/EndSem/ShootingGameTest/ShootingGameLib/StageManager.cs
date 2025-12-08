namespace ShootingGameLib
{
    public class StageManager
    {
        public List<Enemy> EnemyList { get; private set; } = new List<Enemy>();

        public void AddEnemy(Enemy enemy) => EnemyList.Add(enemy);

        public void InsertEnemy(int index, Enemy enemy)
        {
            if (index >= 0 && index <= EnemyList.Count) EnemyList.Insert(index, enemy);
            else EnemyList.Add(enemy);
        }

        public void RemoveEnemyById(int id)
        {
            var target = EnemyList.FirstOrDefault(e => e.SpawnId == id);
            if (target != null) EnemyList.Remove(target);
        }

        public void ClearEnemies() => EnemyList.Clear();
    }
}