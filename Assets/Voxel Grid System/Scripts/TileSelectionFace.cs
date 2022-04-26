using UnityEngine;
using UnityEngine.EventSystems;

namespace VoxelGridSystem
{
    [RequireComponent(typeof(MeshRenderer), typeof(Collider))]
    public class TileSelectionFace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        private TileSelectionMesh tileSelectionMesh;
        private MeshRenderer meshRenderer;
        [SerializeField]
        private TileSelectionMesh.Face face;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
            tileSelectionMesh = transform.parent.GetComponent<TileSelectionMesh>();
        }

        private TileBase GetFacingNeighbor()
        {
            switch (face)
            {
                case TileSelectionMesh.Face.Up:
                    return tileSelectionMesh.Tile.up;
                case TileSelectionMesh.Face.Forward:
                    return tileSelectionMesh.Tile.forward;
                case TileSelectionMesh.Face.Right:
                    return tileSelectionMesh.Tile.right;
                case TileSelectionMesh.Face.Back:
                    return tileSelectionMesh.Tile.back;
                case TileSelectionMesh.Face.Left:
                    return tileSelectionMesh.Tile.left;
                case TileSelectionMesh.Face.Down:
                    return tileSelectionMesh.Tile.down;
                default:
                    return null;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TileBase tile = GetFacingNeighbor();
            if (tile)
                meshRenderer.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            meshRenderer.enabled = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                TileBase tile = GetFacingNeighbor();
                if (tile)
                    tile.Filled = true;
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                tileSelectionMesh.Tile.Filled = false;
            }
        }
    }
}
