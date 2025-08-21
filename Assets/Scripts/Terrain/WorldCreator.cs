using System.Collections.Generic;
using UnityEngine;
using Game.Data;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Game.Terrain
{
    public class WorldCreator : MonoBehaviour
    {
        public List<Vector3> ValidSpawnPoints;
        public World CurrentWorld { get; private set; }

        [SerializeField] int width = 10;
        [SerializeField] int depth = 10;
        [SerializeField] int height = 3;

        [SerializeField] BlockDB blockDataBase;

        private const float _spacing = 2f;

        public void CreateWorld(out Vector3 spawnPoint)
        {
            CurrentWorld = new World();
            CurrentWorld.Blocks = new List<Block>();
            ValidSpawnPoints = new List<Vector3>();


            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Vector3 position = new Vector3(x * _spacing, y * _spacing, z * _spacing);
                        Block blockPrefab = Instantiate(blockDataBase.DirtBlock, position, Quaternion.identity);
                        CurrentWorld.Blocks.Add(blockPrefab);
                    }

                    // Assume the block at (x, height - 1, z) is the top ground block
                    Vector3 spawnCandidate = new Vector3(x * _spacing, (height - 1) * _spacing, z * _spacing);

                    // Only add it to valid list if no tree will be created here
                    if (Random.value < 0.1f)
                    {
                        Vector3 basePos = new Vector3(x * _spacing, height * _spacing, z * _spacing);
                        CreateSimpleTree(basePos);
                    }
                    else
                    {
                        ValidSpawnPoints.Add(spawnCandidate + Vector3.up * _spacing); // spawn 2 units above the surface
                    }
                }
            }

            CurrentWorld.SetParentToBlocks(this.transform);

            // Set spawn point above the center of the world
            spawnPoint = new Vector3((width / 2f) * _spacing, (height + 1) * _spacing, (depth / 2f) * _spacing);
        }

        public void DestoryWorld()
        {
            if (CurrentWorld == null || CurrentWorld.Blocks == null) return;

            foreach (var block in CurrentWorld.Blocks)
            {
                if (block)
                {
                    Destroy(block.gameObject);
                }
            }
        }
        private void CreateSimpleTree(Vector3 basePos)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 pos = basePos + Vector3.up * i * _spacing;
                Block woodPrefab = Instantiate(blockDataBase.WoodBlock, pos, Quaternion.identity);
                CurrentWorld.Blocks.Add(woodPrefab);
            }

            Block logPrefab = Instantiate(blockDataBase.LeafBlock, basePos + Vector3.up * 3 * _spacing, Quaternion.identity);
            CurrentWorld.Blocks.Add(logPrefab);
        }



#if UNITY_EDITOR
        [ContextMenu("Generate World")]
        public void GenerateInEditor()
        {
            DestoryWorld();

            CreateWorld(out _); // ignore spawnPoint here
        }

        [ContextMenu("Destory World")]
        public void DestoryInEditor()
        {
            DestoryWorld();
        }
#endif
    }
}