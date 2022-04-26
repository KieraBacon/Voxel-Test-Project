using UnityEngine;

namespace VoxelGridSystem
{
    [CreateAssetMenu(fileName = "New Voxel Tile Set", menuName = "Voxel Tile System/Voxel Tile Set", order = 2)]
    public class TileSet : ScriptableObject
    {
        private TileData[] tiles;
    }
}
