using System;

namespace TowerDefense.Core
{
    public abstract class BaseUnit : IBaseObject
    {
        public Guid ID { get; } = Guid.NewGuid();
        public abstract string Name { get; }

        public int BaseHealth { get; protected set; } = 100;
        public int BaseAttackPower { get; protected set; } = 10;
        public int BaseRange { get; protected set; } = 5;

        public int HealthLevel { get; private set; } = 1;
        public int AttackLevel { get; private set; } = 1;
        public int RangeLevel { get; private set; } = 1;

        public int BonusHealth { get; protected set; } = 0;
        public int BonusAttackPower { get; protected set; } = 0;

        // Level에 따른 능력치 변화 로직 (예시: 레벨 당 Health +20, Attack +5, Range +1)
        public int CurrentHealth => BaseHealth + (HealthLevel * 20) + BonusHealth;
        public int CurrentAttackPower => BaseAttackPower + (AttackLevel * 5) + BonusAttackPower;
        public int CurrentRange => BaseRange + (RangeLevel * 1);

        protected BaseUnit()
        {
            // 객체 생성 시 초기 레벨을 랜덤하게 설정 (1~3)
            Random rand = new Random();
            HealthLevel = rand.Next(1, 4);
            AttackLevel = rand.Next(1, 4);
            RangeLevel = rand.Next(1, 4);
        }

        public void LevelUp(UnitLevelType type, int amount)
        {
            switch (type)
            {
                case UnitLevelType.Health: HealthLevel += amount; break;
                case UnitLevelType.Attack: AttackLevel += amount; break;
                case UnitLevelType.Range: RangeLevel += amount; break;
            }
            if (HealthLevel < 1) HealthLevel = 1;
            if (AttackLevel < 1) AttackLevel = 1;
            if (RangeLevel < 1) RangeLevel = 1;
        }
    }
}

/*
 * 
 * 문제1. 어디 수정하면 됨
 * 문제2. ㅁㄴㅇㅁㄴㅇㅁㄴㅇ 어디 수정하면 됨.
 * 
 * 
 */