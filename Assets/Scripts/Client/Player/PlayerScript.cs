using UnityEngine;

namespace Game.Client.Player
{
    public class PlayerScript : MonoBehaviour
    {
        [field: SerializeField] public PlayerManager playerManager { get; private set; }

        private void OnValidate()
        {
            if(!playerManager)
            {
                playerManager = GetComponent<PlayerManager>();

                if (!playerManager)
                {
                    playerManager = GetComponentInParent<PlayerManager>();
                }
            }
        }
    }
}
