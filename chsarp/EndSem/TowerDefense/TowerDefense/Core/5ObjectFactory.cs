using System;
using System.Collections.Generic;

namespace TowerDefense.Core
{
    // 요구사항 6: 객체 생성 로직을 캡슐화하여 하드코딩된 new 연산을 방지
    public static class ObjectFactory
    {
        private static readonly Random Rng = new Random();

        public static Tower CreateTower(TowerType type)
        {
            return type switch
            {
                TowerType.Melee => new MeleeTower(),
                TowerType.Ranged => new RangedTower(),
                TowerType.Support => new SupportTower(),
                _ => new RangedTower(),
            };
        }

        public static Enemy CreateEnemy(EnemyType type)
        {
            return type switch
            {
                EnemyType.Normal => new NormalEnemy(),
                EnemyType.MidBoss => new MidBossEnemy(),
                EnemyType.FinalBoss => new FinalBossEnemy(),
                _ => new NormalEnemy(),
            };
        }

        // 요구사항 2: 스테이지에 맞게 초기 객체 생성
        public static List<Tower> CreateInitialTowers(int stage)
        {
            var towers = new List<Tower>();
            int towerCount = 5 + (stage * 2); // Stage 1: 7개 타워

            for (int i = 0; i < towerCount; i++)
            {
                TowerType type = (TowerType)(i % 3); // Melee, Ranged, Support 순환 생성
                towers.Add(CreateTower(type));
            }
            return towers;
        }

        public static List<Enemy> CreateInitialEnemies(int stage)
        {
            var enemies = new List<Enemy>();
            int enemyCount = 15 + (stage * 5); // Stage 1: 20개 적

            for (int i = 1; i <= enemyCount; i++)
            {
                Enemy enemy;
                if (i == enemyCount) enemy = CreateEnemy(EnemyType.FinalBoss);
                else if (i % 7 == 0) enemy = CreateEnemy(EnemyType.MidBoss);
                else enemy = CreateEnemy(EnemyType.Normal);

                enemy.SpawnOrder = i;
                // 초기 좌표 설정
                enemy.SpawnCoordinate = (i * 15, Rng.Next(1, 10));
                enemies.Add(enemy);
            }
            return enemies;
        }
    }
}