using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelGridSystem
{
    [System.Serializable]
    public class TileConnectionRule
    {
        [SerializeField]
        private TileData tile;
        [SerializeField]
        private List<bool> connections;
    }
}
