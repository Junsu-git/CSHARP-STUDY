namespace TowerDefense.Core
{
    public abstract class Tower : BaseUnit
    {
        public override string Name => GetType().Name;
        public abstract TowerType Type { get; }
    }

    public enum TowerType { Melee, Ranged, Support }
}