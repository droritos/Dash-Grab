using System.Collections.Generic;
using Game.Data;
using UnityEngine;

namespace Game.Terrain
{
    /// <summary>
    /// Represents the game world containing blocks.
    /// </summary>
    public class World
    {
        public List<Block> Blocks;

        public void SetParentToBlocks(Transform parent)
        {
            foreach (var block in Blocks)
            {
                block.transform.SetParent(parent);
            }
        }

        //public void DestoryWorld()
        //{
        //    foreach (var block in Blocks)
        //    {
        //        Destroy(block.gameObject);
        //    }
        //}

    }
}

