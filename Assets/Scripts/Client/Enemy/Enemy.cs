using UnityEngine;

namespace Game.Client.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Info")]
        [field: SerializeField] public int WorthValue;

        [Header("Visuals")]
        [SerializeField] Transform visualParent;
        [SerializeField] VisualController visualController;

        [Header("Components")]
        [SerializeField] CharacterController characterController;

        [Header("Movement Settings")]
        [SerializeField, Range(0f, 20f)] float speed = 5f;

        [Header("Gravity Settings")]
        [SerializeField, Range(-50f, 0f)] float gravity = -9.81f;
        [SerializeField, Range(0f, 1f)] float groundCheckDistance = 0.1f;
        [SerializeField] LayerMask groundMask;

        private Transform _playerTarget;

        private float verticalVelocity;
        private bool isGrounded;

        private void Update()
        {
            GoTo();
        }

        public void SetVisualPrefab()
        {
            int childCount = visualParent.childCount;

            if (childCount == 0)
            {
                Debug.LogWarning("No children found under visualParent.");
                return;
            }

            // Deactivate all children
            for (int i = 0; i < childCount; i++)
            {
                visualParent.GetChild(i).gameObject.SetActive(false);
            }

            // Activate one at random
            int randomIndex = Random.Range(0, childCount);
            visualParent.GetChild(randomIndex).gameObject.SetActive(true);
            visualController.SetAnimator(visualParent.GetChild(randomIndex).GetComponent<Animator>());
        }

        public void SetTarget(Transform player)
        {
            _playerTarget = player;
            SetVisualPrefab();
        }

        private void GoTo()
        {
            if (_playerTarget == null) return;

            // Ground check
            isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

            if (isGrounded && verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;
            }

            Vector3 direction = (_playerTarget.position - transform.position);
            direction.y = 0f;
            Vector3 horizontalDirection = direction.normalized;

            if (horizontalDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(horizontalDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            Vector3 horizontalVelocity = horizontalDirection * speed;
            Vector3 finalVelocity = new Vector3(horizontalVelocity.x, verticalVelocity, horizontalVelocity.z);

            characterController.Move(finalVelocity * Time.deltaTime);

            // Correct magnitude — for animation only use horizontal velocity
            visualController.HandleMovement(new Vector3(horizontalVelocity.x, 0, horizontalVelocity.z).magnitude);
        }
    }
}
