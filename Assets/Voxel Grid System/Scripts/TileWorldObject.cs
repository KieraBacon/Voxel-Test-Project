using UnityEngine;

namespace VoxelGridSystem
{
    public class TileWorldObject : MonoBehaviour
    {
        public TileBase tile;
        [SerializeField]
        private new Collider collider;
        public Collider Collider => collider;
        [SerializeField]
        private MeshFilter meshFilter;
        public MeshFilter MeshFilter => meshFilter;
        [SerializeField]
        private MeshRenderer meshRenderer;
        public MeshRenderer MeshRenderer => meshRenderer;
    }
}
