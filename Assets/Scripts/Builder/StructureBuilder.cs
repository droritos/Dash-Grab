using System.Collections.Generic;
using Game.Data;
using UnityEngine;

namespace Builder
{
    // We'll use this for robust parsing
    public class StructureBuilder : MonoBehaviour
    {
        // The previous array is no longer needed if we use the string input
        // public BlockData[] structureBlueprint; 
    
        // 1. New field to hold the raw text blueprint input
        [HideInInspector] // Hide the default string field, we'll draw a text area in the Editor script
        public string blueprintText = ""; 

        [SerializeField] BlockDB blockDB;

        private Dictionary<string, Block> blockPrefabs = new Dictionary<string, Block>();
        
        private const int BLOCK_SPACING_FACTOR = 2; // <-- Define this near the top of the class

        // --- Core Build Method ---
        [ContextMenu("BUILD STRUCTURE PREFAB")]
        public void BuildStructure()
        {
            InitializeBlockPrefabs();

            // 1. Get the list of blocks from the string parser
            List<BlockData> blocksToBuild = ParseBlueprintText(blueprintText);
        
            if (blocksToBuild == null || blocksToBuild.Count == 0)
            {
                Debug.LogError("Blueprint text is empty or contains no valid lines! Nothing to build.");
                return;
            }
        
            // 2. Create an empty GameObject to hold all the blocks
            GameObject structureRoot = new GameObject("NewStructure_Built_FromText");
        
            // 3. Iterate through the parsed blocks and place them
            foreach (BlockData block in blocksToBuild)
            {
                if (blockPrefabs.ContainsKey(block.blockType))
                {
                    Block blockPrefab = blockPrefabs[block.blockType];
                
                    // Instantiate the block at the parsed position
                    Block newBlock = Instantiate(blockPrefab, (Vector3)block.position * BLOCK_SPACING_FACTOR, Quaternion.identity);

                    newBlock.transform.SetParent(structureRoot.transform);
                    newBlock.name = block.blockType + block.position.ToString();
                }
                else
                {
                    Debug.LogWarning($"Block Type '{block.blockType}' not found in Prefab Dictionary. Skipping.");
                }
            }
        
            Debug.Log($"Successfully built a structure with {blocksToBuild.Count} blocks.");
        }
    
        // --- New Parsing Logic ---
        // Inside the StructureBuilder.cs script:

   // Inside the StructureBuilder.cs script:

private List<BlockData> ParseBlueprintText(string blueprint)
{
    List<BlockData> blocks = new List<BlockData>();
    
    // Split by space, carriage return, or newline
    string[] entries = blueprint.Split(
        new[] { ' ', '\r', '\n' }, 
        System.StringSplitOptions.RemoveEmptyEntries
    );

    int entryNumber = 0; // Tracks which block entry we are on
    foreach (string entry in entries)
    {
        entryNumber++; 
        string trimmedEntry = entry.Trim(); 

        if (string.IsNullOrEmpty(trimmedEntry) || trimmedEntry.StartsWith("//") || trimmedEntry.StartsWith("#"))
        {
            continue;
        }

        // Expected format: X,Y,Z,BlockType
        string[] parts = trimmedEntry.Split(','); 
        
        if (parts.Length < 4)
        {
            // CRITICAL DEBUG: Failed to split into 4 parts (likely a missing comma)
            Debug.LogWarning($"[Blueprint Parse FAIL] Entry {entryNumber}: Not enough parts (Expected 4, Got {parts.Length}). Check for missing commas. Entry skipped: '{trimmedEntry}'");
            continue;
        }

        if (int.TryParse(parts[0].Trim(), out int x) &&
            int.TryParse(parts[1].Trim(), out int y) &&
            int.TryParse(parts[2].Trim(), out int z))
        {
            string type = parts[3].Trim().ToLower(); // Ensures case-insensitivity
    
            // BEFORE adding to list, check if the block type is actually in the dictionary.
            // This is a powerful preemptive check.
            if (!blockPrefabs.ContainsKey(type))
            {
                // CRITICAL DEBUG: Block name is NOT recognized. This is the most common AI failure.
                Debug.LogWarning($"[Blueprint Parse FAIL] Entry {entryNumber}: Block Type '{type.ToUpper()}' not recognized. Skipping block at {x},{y},{z}. Check the spelling against the block list!");
                continue; // Skip this block entry
            }

            blocks.Add(new BlockData
            {
                position = new Vector3Int(x, y, z),
                blockType = type
            });
        }
        else
        {
            // CRITICAL DEBUG: Failed to convert a coordinate to an integer
            Debug.LogWarning($"[Blueprint Parse FAIL] Entry {entryNumber}: Invalid coordinate format. Expected integers. Entry skipped: '{trimmedEntry}'");
        }
    }

    return blocks;
}
        
        // Helper to populate the Dictionary from Inspector fields (same as before)
        private void InitializeBlockPrefabs()
        {
            blockPrefabs.Clear();
    
            blockPrefabs.Add("dirt", blockDB.DirtBlock);
            blockPrefabs.Add("leaf", blockDB.LeafBlock);
            blockPrefabs.Add("wood", blockDB.WoodBlock);
            blockPrefabs.Add("crate", blockDB.CrateBlock);
            blockPrefabs.Add("darkstone", blockDB.DarkStoneBlock);
            blockPrefabs.Add("glass", blockDB.GlassBlock);
            blockPrefabs.Add("gravel", blockDB.GravelBlock);
            blockPrefabs.Add("graybrick", blockDB.GrayBrickBlock);
            blockPrefabs.Add("hay", blockDB.HayBlock);
            blockPrefabs.Add("metal", blockDB.MetalBlock);
            blockPrefabs.Add("metalframe", blockDB.MetalFrameBlock);
            blockPrefabs.Add("redbrick", blockDB.RedBrickBlock);
            blockPrefabs.Add("sand", blockDB.SandBlock);
            blockPrefabs.Add("smoothstone", blockDB.SmoothStoneBlock);
            blockPrefabs.Add("snow", blockDB.SnowBlock);
            blockPrefabs.Add("stone", blockDB.StoneBlock);
        }
    }


}