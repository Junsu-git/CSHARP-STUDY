using EndSemProj.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndSemProj.GameObject
{
    // ==========================================
    // [Object] 게임 오브젝트
    // ==========================================
    class Player
    {
        public string Name { get; private set; }
        public int Level { get; private set; } = 1;
        public int HP { get; private set; }
        public int Attack { get; private set; }
        public string WeaponName { get; private set; }
        public int X { get; private set; } = 0;
        public int Y { get; private set; } = 0;

        public Player(string name, int hp, string weapon)
        {
            Name = name;
            HP = hp;
            WeaponName = weapon;
            UpdateStats();
        }

        private void UpdateStats()
        {
            int weaponAtk = DataRepository.Weapons.ContainsKey(WeaponName) ? DataRepository.Weapons[WeaponName] : 0;
            Attack = (Level * 2) + weaponAtk; // 레벨 비례 공격력 공식
        }

        public string Move(int dx, int dy)
        {
            X += dx; Y += dy;
            return $"[{Name}] 이동 -> ({X}, {Y})";
        }

        public void LevelUp(int amount)
        {
            Level += amount;
            HP += amount * 10;
            UpdateStats();
        }

        public void ChangeWeapon(string newWeapon)
        {
            if (DataRepository.Weapons.ContainsKey(newWeapon))
            {
                WeaponName = newWeapon;
                UpdateStats();
            }
        }
    }
}
