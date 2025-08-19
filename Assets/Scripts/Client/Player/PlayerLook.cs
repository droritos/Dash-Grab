using UnityEngine;

namespace Game.Client.Player
{
    public class PlayerLook : PlayerScript
    {
        [SerializeField] private float sensitivity = 2f;
        [SerializeField] private float verticalClamp = 80f;

        private Transform _cameraTransform;
        private Transform _playerTransform; // Add this for horizontal rotation
        private PlayerInput _input;
        private float pitch = 0f;
        private Vector3 _lookDirection;

        private void OnEnable()
        {
            _input = playerManager.PlayerInput;
            _cameraTransform = playerManager.MainCamera.transform;
            _playerTransform = playerManager.transform;
            _lookDirection = _cameraTransform.forward;
        }

        private void Update()
        {
            Vector2 lookInput = _input.LookInput;

            // Horizontal (yaw) - rotate the player body around Y axis
            _playerTransform.Rotate(Vector3.up * lookInput.x * sensitivity);

            // Vertical (pitch) - rotate the camera around X axis
            pitch -= lookInput.y * sensitivity;
            pitch = Mathf.Clamp(pitch, -verticalClamp, verticalClamp);
            _cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

            // Update look direction
            _lookDirection = _cameraTransform.forward;
        }
    }
}