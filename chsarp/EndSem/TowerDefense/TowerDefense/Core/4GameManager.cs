using System;
using System.Collections.Generic;
using System.Linq;

namespace TowerDefense.Core
{
    // 요구사항 1: 게임 관리 역할 (TEST 프로그램에서 핵심 모니터링 관리)
    public class GameManager
    {
        public List<Tower> Towers { get; private set; }
        public List<Enemy> Enemies { get; private set; }

        public GameManager(int stage = 1)
        {
            Towers = ObjectFactory.CreateInitialTowers(stage);
            Enemies = ObjectFactory.CreateInitialEnemies(stage);
            Console.WriteLine($"[GameManager] Stage {stage} 준비 완료. 타워: {Towers.Count}개, 적: {Enemies.Count}개");
        }

        // --- 9.a: 플레이어 타워 테스트 기능 ---

        public (int totalCount, string list) GetTowerListInfo()
        {
            string list = string.Join("\n", Towers.Select((t, index) =>
                $"{index + 1}. [ID:{t.ID.ToString().Substring(0, 8)}] Type:{t.Type,-8} Att:{t.CurrentAttackPower,-4} Rng:{t.CurrentRange,-3} Lv:{t.AttackLevel,-2} Health:{t.CurrentHealth,-4}"));
            return (Towers.Count, list);
        }

        public Tower GetTowerByID(Guid id) => Towers.FirstOrDefault(t => t.ID == id);

        // ID 또는 인덱스를 사용하여 타워 찾기
        public Tower FindTower(string identifier)
        {
            if (Guid.TryParse(identifier, out Guid id)) return Towers.FirstOrDefault(t => t.ID == id);
            if (int.TryParse(identifier, out int index) && index > 0 && index <= Towers.Count)
                return Towers[index - 1];
            return Towers.FirstOrDefault(t => t.ID.ToString().StartsWith(identifier));
        }

        public bool RemoveTower(Guid id) => Towers.Remove(Towers.FirstOrDefault(t => t.ID == id));

        public bool ChangeTowerType(Guid id, TowerType newType)
        {
            int index = Towers.FindIndex(t => t.ID == id);
            if (index != -1)
            {
                Towers[index] = ObjectFactory.CreateTower(newType);
                return true;
            }
            return false;
        }

        public void AddTower(TowerType type) => Towers.Add(ObjectFactory.CreateTower(type));

        // --- 9.b: 적 목록 테스트 기능 ---

        public (int totalCount, string list) GetEnemyListInfo()
        {
            // 요구사항 5, 9.b: 출현 순서대로 출력
            string list = string.Join("\n", Enemies.OrderBy(e => e.SpawnOrder).Select((e, index) =>
                $"{index + 1}. [ID:{e.ID.ToString().Substring(0, 8)}] Type:{e.Type,-10} Lv:{e.AttackLevel,-2} Order:{e.SpawnOrder,-3} Pos:({e.SpawnCoordinate.x},{e.SpawnCoordinate.y}) Pattern:'{e.BehaviorPattern}'"));
            return (Enemies.Count, list);
        }

        // ID 또는 인덱스를 사용하여 적 찾기
        public Enemy FindEnemy(string identifier)
        {
            var orderedEnemies = Enemies.OrderBy(e => e.SpawnOrder).ToList();
            if (Guid.TryParse(identifier, out Guid id)) return Enemies.FirstOrDefault(e => e.ID == id);
            if (int.TryParse(identifier, out int index) && index > 0 && index <= orderedEnemies.Count)
                return orderedEnemies[index - 1];
            return Enemies.FirstOrDefault(e => e.ID.ToString().StartsWith(identifier));
        }

        public bool RemoveEnemy(Guid id) => Enemies.Remove(Enemies.FirstOrDefault(e => e.ID == id));

        public void AddEnemy(EnemyType type, int spawnOrder, (int x, int y) coords)
        {
            var newEnemy = ObjectFactory.CreateEnemy(type);
            newEnemy.SpawnOrder = spawnOrder;
            newEnemy.SpawnCoordinate = coords;
            Enemies.Add(newEnemy);
        }

        // --- 9.c: 객체 Level 변경 기능 ---

        public bool ChangeAttackLevel(Guid id, int levelAmount, bool isTower)
        {
            BaseUnit unit = isTower
                ? Towers.FirstOrDefault(t => t.ID == id)
                : Enemies.FirstOrDefault(e => e.ID == id);

            if (unit != null)
            {
                // 요구사항 9.c: 플레이어 타워의 공격력 레벨만 변경, 적의 공격력 레벨만 변경
                unit.LevelUp(UnitLevelType.Attack, levelAmount);
                return true;
            }
            return false;
        }
    }
}