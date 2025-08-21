using UnityEngine;

namespace Game.Data
{
    /// <summary>
    /// Represents a block that can be picked up in the game world.
    /// </summary>
    [RequireComponent(typeof(BoxCollider))]
    public class PickUpBlock : Block
    {
        [field: SerializeField] public int WorthValue { get; private set; } = 1;

    }
}
