using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelProject.GridSystem
{
    public class VoxelTileWorldObject : MonoBehaviour
    {
        public VoxelTile tile;
        [SerializeField]
        private Collider collider;
        public Collider Collider => collider;
        [SerializeField]
        private MeshFilter meshFilter;
        public MeshFilter MeshFilter => meshFilter;
        [SerializeField]
        private MeshRenderer meshRenderer;
        public MeshRenderer MeshRenderer => meshRenderer;
    }
}
