using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "BlockDB", menuName = "Terrain/Block Data Base")]
    public class BlockDB : ScriptableObject
    {
        // --- Existing Blocks ---
        public Block DirtBlock;
        public Block LeafBlock;
        public Block WoodBlock; // AKA Log block

        // --- NEW BLOCKS ---
        public Block CrateBlock;
        public Block DarkStoneBlock;
        public Block GlassBlock;
        public Block GravelBlock;
        public Block GrayBrickBlock;
        public Block HayBlock;
        public Block MetalBlock;
        public Block MetalFrameBlock;
        public Block RedBrickBlock;
        public Block SandBlock;
        public Block SmoothStoneBlock;
        public Block SnowBlock;
        public Block StoneBlock; // Assuming this is the cracked/textured stone one
        
        // Note: The "Block" in the top left is usually the base class/prefab,
        // so I didn't add a "Block" field unless you have a specific use for it.
    }
}