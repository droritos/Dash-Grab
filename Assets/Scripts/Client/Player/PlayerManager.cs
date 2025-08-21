using UnityEngine;
using Game.Managers;

namespace Game.Client.Player
{ 
    public class PlayerManager : MonoBehaviour
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
        [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }

        [field: SerializeField] public PlayerLook PlayerLook { get; private set; }
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public VisualController VisualController { get; private set; }

        private void OnEnable()
        {
            Vector3 vector3 = GameManager.Instance.PlayerSpawnPoint;
            this.transform.position = new Vector3(vector3.x, vector3.y + 5f, vector3.z); // Reset position to a default value
        }

        private void OnValidate()
        {
            if (!PlayerInput)
            {
                PlayerInput = GetComponentInChildren<PlayerInput>();
            }
            if (!PlayerMovement)
            {
                PlayerMovement = GetComponentInChildren<PlayerMovement>();
            }
            if (!PlayerLook)
            {
                PlayerLook = GetComponentInChildren<PlayerLook>();
            }
            if (!MainCamera)
            {
                MainCamera = Camera.main;
            }

            if(!VisualController)
                VisualController = GetComponentInChildren<VisualController>();
        }
    }
}