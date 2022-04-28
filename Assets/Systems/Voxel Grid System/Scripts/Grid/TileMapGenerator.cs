using UnityEngine;

namespace VoxelGridSystem
{
    public class TileMapGenerator : MonoBehaviour
    {
        [SerializeField]
        private Mesh mesh;
        [SerializeField]
        private TileGrid grid;
        [SerializeField]
        private float V = 0.95f;

        protected virtual void Start()
        {
            Generate();
        }

        public virtual void Generate()
        {
            if (!grid) return;

            foreach (TileBase gridSpace in grid.tiles)
            {
                if (Random.Range(0.0f, 1.0f) > V)
                {
                    gridSpace.Filled = true;
                }
            }
        }
    }
}
