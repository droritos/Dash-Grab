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
        public List<Block> Blocks = new();

        public Vector3 MinYlevel { get; private set; }
        public Vector3 MinXlevel { get; private set; }
        public Vector3 MaxXlevel { get; private set; }
        public Vector3 MaxZlevel { get; private set; }
        public Vector3 MinZlevel { get; private set; }


        /// <summary>
        /// Some sort of inzilizer for the world data.
        /// </summary>
        /// <param name="parent"></param>
        public void SetParentToBlocks(Transform parent)
        {
            foreach (var block in Blocks)
            {
                block.transform.SetParent(parent);
            }
            InitializeData();
        }

        private void InitializeData()
        {
            foreach (var block in Blocks)
            {
                Vector3 point = block.transform.position;

                if (point.y < MinYlevel.y)
                {
                    MinYlevel = point;
                }

                if (point.x < MinXlevel.x)
                {
                    MinXlevel = point;
                }

                if (point.x > MaxXlevel.x)
                {
                    MaxXlevel = point;
                }

                if (point.z < MinZlevel.x)
                {
                    MinZlevel = point;
                }

                if (point.z > MaxZlevel.x)
                {
                    MaxZlevel = point;
                }
            }
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

