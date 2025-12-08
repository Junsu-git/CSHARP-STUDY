using System;

namespace TowerDefense.Core
{
    public abstract class Enemy : BaseUnit
    {

        public override string Name => GetType().Name;
        public int SpawnOrder { get; set; }
        public (int x, int y) SpawnCoordinate { get; set; }
        public string BehaviorPattern { get; set; }
        public abstract EnemyType Type { get; }
    }

    public enum EnemyType { Normal, MidBoss, FinalBoss }
}