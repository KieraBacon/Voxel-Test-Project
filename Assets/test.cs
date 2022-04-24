using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VoxelProject
{
    public class test : MonoBehaviour, UnityEngine.EventSystems.IPointerMoveHandler
    {
        public void OnPointerMove(PointerEventData eventData)
        {
            Debug.Log(gameObject.name);
        }
    }
}
