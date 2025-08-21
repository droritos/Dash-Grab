using UnityEngine;
using Game.Managers;

namespace Game.Client.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField] public float WorthValue; 

        [SerializeField] Transform visualParent;
        [SerializeField] VisualController visualController;

        [SerializeField] CharacterController characterController;

        private Transform _playerTarget;

        [SerializeField] float speed = 5f;

        private void Update()
        {
            GoTo();
        }
        public void SetVisualPrefab(GameObject enemyObject)
        {
            Instantiate(enemyObject, visualParent);
        }

        public void SetTarget(Transform player)
        {
            _playerTarget = player;
        }

        private void GoTo()
        {
            if (_playerTarget == null) return;

            Vector3 direction = (_playerTarget.position - transform.position).normalized;
            Vector3 velocity = direction * speed * Time.deltaTime;

            characterController.Move(velocity);

            visualController.HandleMovement(velocity.magnitude);
        }
    }
}