using UnityEngine;

namespace Game.Client.Player
{
    public class PlayerMovement : PlayerScript
    {
        [SerializeField] CharacterController characterController;
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float dashSpeed = 20f;
        [SerializeField] float dashDuration = 0.2f;
        [SerializeField] float gravity = -9.81f;


        private PlayerInput _input;
        private bool isDashing = false;
        private float dashTimer;
        private Vector3 verticalVelocity;

        void OnEnable()
        {
            _input = playerManager.PlayerInput;
        }

        void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (isDashing)
            {
                characterController.Move(transform.forward * dashSpeed * Time.deltaTime);
                dashTimer -= Time.deltaTime;

                if (dashTimer <= 0)
                    isDashing = false;

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
        }

        public void StartDash()
        {
            isDashing = true;
            dashTimer = dashDuration;
        }

        public bool IsDashing => isDashing;
    }
}
