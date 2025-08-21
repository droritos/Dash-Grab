using System.Collections;
using UnityEngine;

namespace Game.Client.Player
{
    public class PlayerMovement : PlayerScript
    {
        public bool IsDashing {  get; private set; } = false;

        [Header("Components")]
        [SerializeField] CharacterController characterController;

        [Header("Movement Settings")]
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float gravity = -9.81f;

        [Header("Dash Settings")]
        [SerializeField] float dashSpeed = 20f;
        [SerializeField] float dashDuration = 0.2f;
        [SerializeField] float dashCooldown = 0.2f;

        #region Private Fields

        private PlayerInput _input;
        private float _dashTimer;
        private Vector3 verticalVelocity;
        private bool _canDash = true;

        #endregion
        void OnEnable()
        {
            _input = playerManager.PlayerInput;
        }

        void Update()
        {
            HandleMovement();
        }
        public void StartDash()
        {
            if(!_canDash) return;

            IsDashing = true;
            _dashTimer = dashDuration;

            StartCoroutine(DashCooldown());
        }
        private void HandleMovement()
        {
            if (IsDashing)
            {
                characterController.Move(transform.forward * dashSpeed * Time.deltaTime);
                _dashTimer -= Time.deltaTime;

                if (_dashTimer <= 0)
                    IsDashing = false;

                return;
            }

            Vector2 moveInput = _input.MoveInput;
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

            // Gravity logic
            if (characterController.isGrounded && verticalVelocity.y < 0)
            {
                verticalVelocity.y = -2f; // Small value to keep grounded
            }
            else
            {
                verticalVelocity.y += gravity * Time.deltaTime;
            }

            // Combine movement and gravity
            Vector3 totalMove = (move * moveSpeed) + verticalVelocity;
            characterController.Move(totalMove * Time.deltaTime);

            playerManager.VisualController.HandleMovement(moveInput.magnitude);
        }
        private IEnumerator DashCooldown()
        {
            _canDash = false;
            yield return new WaitForSeconds(this.dashCooldown);
            _canDash = true;
        }
    }
}
