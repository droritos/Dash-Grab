using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Client.Player
{
    public class PlayerInput : PlayerScript
    {
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }

        private InputSystemActions _inputSystemActions;


        private void Awake() => _inputSystemActions = new InputSystemActions();

        #region << Enable/Disable Input System >>
        private void OnEnable()
        {
            SetupInput();
        }

        private void OnDisable()
        {
            CleanupInput();
        }
        #endregion
        private void SetupInput()
        {
            _inputSystemActions.Player.Move.performed += OnMovePerformed;
            _inputSystemActions.Player.Move.canceled += OnMoveCancelled;

            _inputSystemActions.Player.Sprint.performed += OnDash;

            SetupLook(true);

            _inputSystemActions.Enable();
        }

        private void CleanupInput()
        {
            _inputSystemActions.Disable();
            _inputSystemActions.Player.Move.performed -= OnMovePerformed;
            _inputSystemActions.Player.Move.canceled -= OnMoveCancelled;

            SetupLook(false);

            _inputSystemActions.Player.Sprint.performed -= OnDash;

        }

        private void OnMovePerformed(InputAction.CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>();
        private void OnMoveCancelled(InputAction.CallbackContext ctx) => MoveInput = Vector2.zero;

        private void OnDash(InputAction.CallbackContext ctx)
        {
            playerManager.PlayerMovement.StartDash();
        }
        private void SetupLook(bool state)
        {
            if (state)
            {
                _inputSystemActions.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
                _inputSystemActions.Player.Look.canceled += ctx => LookInput = Vector2.zero;
            }
            else
            {
                _inputSystemActions.Player.Look.performed -= ctx => LookInput = ctx.ReadValue<Vector2>();
                _inputSystemActions.Player.Look.canceled -= ctx => LookInput = Vector2.zero;
            }
        }

        private void OnDestroy()
        {
            // Ensure cleanup happens even if Despawned wasn't called
            if (_inputSystemActions != null)
            {
                _inputSystemActions.Disable();
                _inputSystemActions?.Dispose();
            }
        }
    }

}