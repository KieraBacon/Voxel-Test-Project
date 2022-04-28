using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelGridSystem
{
    [CreateAssetMenu(fileName = "New Rule Tile", menuName = "Voxel Tile System/Rule Tile", order = 2)]
    public class RuleTile : ScriptableObject
    {
        [SerializeField]
        private TileData defaultTile;
        [SerializeField]
        private List<TileConnectionRule> connectionRules;
    }
}
