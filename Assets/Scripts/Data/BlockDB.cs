using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "BlockDB", menuName = "Terrain/Block Data Base")]
    public class BlockDB : ScriptableObject
    {
        public Block DirtBlock;
        public Block LeafBlock;
        public Block WoodBlock; // AKA Log block
    }
}