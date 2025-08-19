using UnityEngine;

namespace Game.Data
{
    /// <summary>
    /// Represents a block in the game world.
    /// </summary>

    [RequireComponent(typeof(BoxCollider))]
    public class Block : MonoBehaviour
    {
        [SerializeField] Collider blockCollider;

        private void OnValidate()
        {
            if (!blockCollider)
                blockCollider = GetComponent<Collider>();

            if (!blockCollider)
            {
                Debug.LogError($"Missing Collider on {gameObject.name}!", this);
                return;
            }

            // Only auto-fix if BoxCollider and size is wrong
            if (blockCollider is BoxCollider box)
            {
                Vector3 size = box.size;

                if (!Mathf.Approximately(size.x, 2f) || !Mathf.Approximately(size.y, 2f) || !Mathf.Approximately(size.z, 2f))
                {
                    box.size = Vector3.one * 2;
                    box.center = Vector3.zero;
                    Debug.Log($"Auto-fixed BoxCollider size on {gameObject.name}", this);
                }
            }
        }
    }
}
