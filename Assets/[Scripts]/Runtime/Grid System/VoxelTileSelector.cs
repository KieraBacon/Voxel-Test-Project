using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VoxelProject.GridSystem
{
    public class VoxelTileSelector : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IPointerDownHandler
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

        public VoxelTile tile;
        [SerializeField]
        private MeshFilter meshFilter;
        [SerializeField]
        private MeshRenderer up;
        [SerializeField]
        private MeshRenderer forward;
        [SerializeField]
        private MeshRenderer right;
        [SerializeField]
        private MeshRenderer back;
        [SerializeField]
        private MeshRenderer left;
        [SerializeField]
        private MeshRenderer down;
        private MeshRenderer[] MeshRenderers => new MeshRenderer[] { up, forward, right, back, left, down };
        private Face highlight = Face.None;
        public Face Highlighted
        {
            set
            {
                highlight = value;
                //MeshRenderer[] renderers = MeshRenderers;
                //for (int i = 0; i < renderers.Length; i++)
                //{
                //    renderers[i].enabled = i == (int)value;
                //}
            }
            get => highlight;
        }

        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    //Highlighted = true;
        //    Vector3 normal = eventData.pointerPressRaycast.worldNormal;
        //    Debug.Log(normal);
        //}
        //
        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    Highlighted = Face.None;
        //}
        //
        //public void OnDrag(PointerEventData eventData)
        //{
        //    Debug.Log(tile.position + " OnDrag");
        //}
        //
        //public void OnPointerDown(PointerEventData eventData)
        //{
        //    switch (eventData.button)
        //    {
        //        case PointerEventData.InputButton.Left:
        //            tile.Filled = true;
        //            break;
        //        case PointerEventData.InputButton.Right:
        //            break;
        //    }
        //}
    }
}
