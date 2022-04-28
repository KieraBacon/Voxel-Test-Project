using UnityEngine;

namespace VoxelGridSystem
{
    [CreateAssetMenu(fileName = "New Tile Data", menuName = "Voxel Tile System/Tile Data", order = 1)]
    public class TileData : ScriptableObject
    {
        [SerializeField]
        private Mesh mesh;
        public Mesh Mesh => mesh;

        [SerializeField]
        private Quaternion rotation;
        public Quaternion Rotation => rotation;
    }
}
