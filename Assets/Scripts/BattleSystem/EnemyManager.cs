using System.Collections.Generic;
using System.Linq;
using OrbitalSystem;
using UnityEngine;
using Utils;

namespace BattleSystem
{
    public class EnemyManager : MonoBehaviour
    {
        private EnemyWaveData waveData;
        private GameObject enemyPrefab;
        private GameObject spawnPoint;

        private Transform target => ServiceLocator.Get<OrbitalManager>().GetTarget();
        private UpdateProvider UpdateProvider => ServiceLocator.Get<UpdateProvider>();
        private DelayCallService DelayCallService => ServiceLocator.Get<DelayCallService>();

        private List<EnemySpawnPoint> spawnPoints = new List<EnemySpawnPoint>();
        private List<Enemy> enemies = new List<Enemy>();
        private Vector3 spawnPositionOne = new Vector3(-400, 0 ,-200);
        private Vector3 spawnPositionTwo = new Vector3(400, 0 ,-200);
        private Vector3 spawnPointRotation = new Vector3(90,0,0);

        public int CurrentWave = 0;
        
        public void Init(int wave)
        {
            CurrentWave = wave;
            
            LoadResources();
            CreateSpawnPoint();
            CreateWave();
            UpdateProvider.UpdateEvent += OnUpdate;
        }

        private void OnUpdate()
        {
            for (var i = 0; i < enemies.Count; i++)
            {
                var enemy = enemies[i];

                enemy.Move();
            }
        }

        private void LoadResources()
        {
            enemyPrefab = Resources.Load<GameObject>(Constants.EnemyPrefabPath);
            spawnPoint = Resources.Load<GameObject>(Constants.SpawnPointPath);
            waveData = Resources.Load<EnemyWaveData>(Constants.WaveDataPath);
        }

        private void CreateSpawnPoint()
        {
            var firstPoint = Instantiate(spawnPoint, spawnPositionOne, Quaternion.Euler(spawnPointRotation)).GetComponent<EnemySpawnPoint>();
            var secondPoint = Instantiate(spawnPoint, spawnPositionTwo, Quaternion.Euler(spawnPointRotation)).GetComponent<EnemySpawnPoint>();
            
            spawnPoints.AddRange(new []
            {
                firstPoint, secondPoint
            });
        }

        private void CreateWave()
        {
            for (var i = 0; i < waveData.Count * CurrentWave; i++)
            {
                DelayCallService.AddTick(Random.Range(0f, 5f), () =>
                {
                    var enemy = CreateEnemy();
                
                    enemies.Add(enemy);
                });
            }
        }

        private Enemy CreateEnemy()
        {
            var enemy = Instantiate(enemyPrefab).GetComponentInChildren<Enemy>();

            enemy.transform.position = spawnPoints.ElementAt(Random.Range(0, spawnPoints.Count)).transform.position;
            enemy.Init(new EnemyWave(waveData, CurrentWave), target);
            enemy.OnDestroyed += EnemyDestroyed;
            
            return enemy;
        }

        private void EnemyDestroyed(Enemy enemy)
        {
            enemies.Remove(enemy);
            
            Destroy(enemy.gameObject);

            if (enemies.Count == 0)
            {
                CurrentWave += 1;

                DelayCallService.AddTick(3, CreateWave);
            }
        }
    }
}