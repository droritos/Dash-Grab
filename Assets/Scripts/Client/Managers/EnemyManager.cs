using Game.Client.Enemy;
using Game.Data;
using Game.Terrain;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Managers
{

    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] List<GameObject> enemyVisuals;
        [SerializeField] Enemy enemyPrefab;
        private World _currentWorld;

        [SerializeField] int maxEnemies = 100;

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

        private void SetEnemyVisual(Enemy enemy)
        {
            //GameObject visual = Instantiate(Random.Range(enemyVisuals[0], enemyVisuals[enemyVisuals.Count]));
        }


    }
}
