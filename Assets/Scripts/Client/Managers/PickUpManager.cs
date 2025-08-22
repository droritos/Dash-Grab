using UnityEngine;
using System.Collections.Generic;
using Game.Data;
using Game.Terrain;

namespace Game.Managers
{
    /// <summary>
    /// Manages the pick-up blocks in the game world.
    /// </summary>
    public class PickUpManager : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] PickUpBlock GoldPickUp;
        [SerializeField] PickUpBlock SilverPickUp;
        [SerializeField] PickUpBlock CopperPickUp;

        [Header("Factory Details")]
        [SerializeField] int _maxPickUps = 100;
        [SerializeField] float _pickUpSpawnRadius = 10f;

        [Header("Chances")]
        [SerializeField] float _goldChance, _silverChance, _copperChance;

        private World _currentWorld;

        public void Start()
        {
            _currentWorld = GameManager.Instance.WorldCreator.CurrentWorld;

            ValidSpawnCreation();

            for (int i = 0; i < _maxPickUps; i++)
            {
                Vector3 randomPosition = GameManager.Instance.GetRandomSpawnPosition();
                PickUpBlock pickUp = CreatePickUps();
                pickUp.transform.position = randomPosition;
            }

            EventObserver.OnPickUpCollected += OnCollected;
        }
        private void OnDisable()
        {
            EventObserver.OnPickUpCollected -= OnCollected;
        }

        private void ValidSpawnCreation()
        {
            if (_currentWorld == null)
            {
                Debug.LogError("Current world is not set. Please create a world first.");
                return;
            }
        }
        private void OnCollected(PickUpBlock pickUpBlock)
        {
            Destroy(pickUpBlock.gameObject);
        }
        private PickUpBlock CreatePickUps()
        {
            return Random.Range(0f, 1f) switch
            {
                var chance when chance < _goldChance => Instantiate(GoldPickUp, this.transform),
                var chance when chance < _silverChance => Instantiate(SilverPickUp,this.transform),
                _ => Instantiate(CopperPickUp,this.transform)
            };
        }
    }
}
