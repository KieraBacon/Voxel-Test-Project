using UnityEngine;

namespace VoxelGridSystem
{
    public class TileSelectionMesh : MonoBehaviour
    {
        public enum Face
        {
            None = -1,
            Up,
            Forward,
            Right,
            Back,
            Left,
            Down,
        }

        private TileBase tile;
        public TileBase Tile => tile;

        private void Awake()
        {
            tile = transform.parent.GetComponent<TileBase>();
        }
    }
}
