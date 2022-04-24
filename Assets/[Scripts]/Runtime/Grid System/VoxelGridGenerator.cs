using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelProject.GridSystem
{
    public class VoxelGridGenerator : MonoBehaviour
    {
        [SerializeField]
        private Mesh mesh;
        [SerializeField]
        private VoxelGrid grid;
        [SerializeField]
        private float V = 0.95f;

        void Start()
        {
            foreach (VoxelTile tile in grid.tiles)
            {
                if (Random.Range(0.0f, 1.0f) > V)
                {
                    tile.Filled = true;
                }
            }
        }
    }
}
