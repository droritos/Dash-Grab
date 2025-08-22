using Game.Data;
using UnityEngine;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;

namespace Game.Client.Player
{
    public class PlayerCollision : PlayerScript
    {
        [SerializeField] CharacterController characterController;

        private void OnTriggerEnter(Collider other)
        {
            if (playerManager.PlayerMovement.IsDashing && other.TryGetComponent<PickUpBlock>(out var pickUp))
            {
                EventObserver.OnPickUpCollected?.Invoke(pickUp);
            }
            if (other.gameObject.TryGetComponent<Enemy.Enemy>(out var enemy))
            {
                EventObserver.OnEnemyHit?.Invoke(enemy);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
        }
    }
}