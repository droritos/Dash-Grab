using Game.Data;
using Game.Managers;
using UnityEngine;

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

        /*
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            EventObserver.OnEnemyHit?.Invoke(enemy);
        }
        */
    }
}
}