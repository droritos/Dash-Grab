using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Client
{

    public class PlayerInput : MonoBehaviour
    {
        private InputSystemActions _inputSystemActions;
        private Vector2 _move;

        private void Awake() => _inputSystemActions = new InputSystemActions();

        private void SetupInput()
        {
            _inputSystemActions.Player.Move.performed += OnMovePerformed;
            _inputSystemActions.Player.Move.canceled += OnMoveCancelled;
            _inputSystemActions.Enable();
        }

        private void CleanupInput()
        {
            _inputSystemActions.Disable();
            _inputSystemActions.Player.Move.performed -= OnMovePerformed;
            _inputSystemActions.Player.Move.canceled -= OnMoveCancelled;
        }

        private void OnMovePerformed(InputAction.CallbackContext ctx) => _move = ctx.ReadValue<Vector2>();
        private void OnMoveCancelled(InputAction.CallbackContext ctx) => _move = Vector2.zero;

        public void OnInput()
        {
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