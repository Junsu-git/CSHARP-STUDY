namespace ShootingGameLib
{
    public enum EnemyType { Normal, MidBoss, FinalBoss }

    public class Enemy : GameUnit
    {
        public int SpawnId { get; set; }
        public int SpawnOrder { get; set; }
        public string Pattern { get; set; }
        public EnemyType Type { get; set; }

        public Enemy(string name, int hp, int atk, EnemyType type) : base(name, hp, atk)
        {
            Type = type;
        }
        public override void Action() { }
    }
}