using Game.Client.Enemy;
using Game.Data;
using Game.Terrain;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Managers
{

    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] Enemy enemyPrefab;
        [SerializeField] int maxEnemies = 100;

        public void Start()
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                Vector3 randomPosition = GameManager.Instance.GetRandomSpawnPosition();
                Enemy enemy = CreateEnemy();
                enemy.transform.position = randomPosition;
            }

            EventObserver.OnEnemyHit += OnEnemyCollide;
        }

        private void OnEnemyCollide(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }

        private Enemy CreateEnemy()
        {
            Enemy enemy = Instantiate(enemyPrefab,this.transform);
            enemy.SetTarget(GameManager.Instance.PlayerManager.transform);

            return enemy;
        }
    }
}
