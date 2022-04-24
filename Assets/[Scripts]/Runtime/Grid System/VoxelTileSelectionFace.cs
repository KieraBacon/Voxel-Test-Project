using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VoxelProject.GridSystem
{
    [RequireComponent(typeof(MeshRenderer))]
    public class VoxelTileSelectionFace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        private VoxelTileSelector tileSelector;
        private MeshRenderer meshRenderer;
        [SerializeField]
        private VoxelTileSelector.Face face;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            tileSelector = GetComponentInParent<VoxelTileSelector>();
        }

        private VoxelTile GetFacingNeighbor()
        {
            switch (face)
            {
                case VoxelTileSelector.Face.Up:
                    return tileSelector.tile.up;
                case VoxelTileSelector.Face.Forward:
                    return tileSelector.tile.forward;
                case VoxelTileSelector.Face.Right:
                    return tileSelector.tile.right;
                case VoxelTileSelector.Face.Back:
                    return tileSelector.tile.back;
                case VoxelTileSelector.Face.Left:
                    return tileSelector.tile.left;
                case VoxelTileSelector.Face.Down:
                    return tileSelector.tile.down;
                default:
                    return null;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            VoxelTile tile = GetFacingNeighbor();
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
                VoxelTile tile = GetFacingNeighbor();
                if (tile)
                    tile.Filled = true;
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                tileSelector.tile.Filled = false;
            }

        }
    }
}
