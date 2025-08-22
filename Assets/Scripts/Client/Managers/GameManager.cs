using UnityEngine;
using Game.Terrain;
using Game.Client.Player;
using NUnit.Framework;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [field: SerializeField] public WorldCreator WorldCreator { get; private set; }
        [field: SerializeField] public PickUpManager PickUpManager { get; private set; }
        [field: SerializeField] public PlayerManager PlayerManager { get; private set; }

        public Vector3 PlayerSpawnPoint { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            StartGame();
        }

        private void OnDisable()
        {
            EndGame();
        }

        public Vector3 GetRandomSpawnPosition()
        {
            var spawnPoints = WorldCreator.ValidSpawnPoints;

            if (spawnPoints == null || spawnPoints.Count == 0)
                return Vector3.zero;

            return spawnPoints[Random.Range(0, spawnPoints.Count)];
        }

        // Add any game management methods here
        private void StartGame()
        {
            WorldCreator.CreateWorld(out Vector3 spawnPoint);
            PlayerSpawnPoint = spawnPoint;

            //PickUpManager.StartManager();
        }
        private void EndGame()
        {
            WorldCreator.DestoryWorld();
        }

        private void OnValidate()
        {
            if (!WorldCreator)
            {
                WorldCreator = FindAnyObjectByType<WorldCreator>();
            }

            if (!PlayerManager)
            {
                PlayerManager = FindAnyObjectByType<PlayerManager>();
            }
        }
    }
}
