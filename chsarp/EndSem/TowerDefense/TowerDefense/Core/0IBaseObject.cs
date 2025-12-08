using System;

namespace TowerDefense.Core
{
    // 유닛별 레벨마다 올라갈? 수치인거 같아요
    public enum UnitLevelType { Health, Attack, Range }

    public interface IBaseObject
    {
        Guid ID { get; }
        string Name { get; }

        int BaseHealth { get; }
        int BaseAttackPower { get; }
        int BaseRange { get; }

        int CurrentHealth { get; }
        int CurrentAttackPower { get; }
        int CurrentRange { get; }

        int HealthLevel { get; }
        int AttackLevel { get; }
        int RangeLevel { get; }

        int BonusHealth { get; }
        int BonusAttackPower { get; }

        void LevelUp(UnitLevelType type, int amount);
    }
}