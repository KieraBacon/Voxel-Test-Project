using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VoxelProject.GridSystem
{
    public class VoxelTile : MonoBehaviour
    {
        [SerializeField]
        private VoxelTileSelector selector;
        [SerializeField]
        private VoxelTileWorldObject worldObject;
        public VoxelGrid grid { get; private set; }
        public Vector3Int position { get; private set; }
        public bool Filled
        {
            get => worldObject.gameObject.activeInHierarchy;
            set
            {
                worldObject.gameObject.SetActive(value);
                Selectable = value;
            }
        }

        public bool Selectable
        {
            get => selector.gameObject.activeInHierarchy;
            set
            {
                selector.gameObject.SetActive(value);
            }
        }

        public void Init(VoxelGrid grid, Vector3Int position)
        {
            this.grid = grid;
            this.position = position;
            Filled = false;
            Selectable = false;
        }

        #region Neighbors
        public VoxelTile up => grid.GetTileAtIndex(position + Vector3Int.up);
        public VoxelTile down => grid.GetTileAtIndex(position + Vector3Int.down);
        public VoxelTile left => grid.GetTileAtIndex(position + Vector3Int.left);
        public VoxelTile right => grid.GetTileAtIndex(position + Vector3Int.right);
        public VoxelTile forward => grid.GetTileAtIndex(position + Vector3Int.forward);
        public VoxelTile back => grid.GetTileAtIndex(position + Vector3Int.back);
        public VoxelTile upleft => grid.GetTileAtIndex(position + Vector3Int.up + Vector3Int.left);
        public VoxelTile upright => grid.GetTileAtIndex(position + Vector3Int.up + Vector3Int.right);
        public VoxelTile upforward => grid.GetTileAtIndex(position + Vector3Int.up + Vector3Int.forward);
        public VoxelTile upback => grid.GetTileAtIndex(position + Vector3Int.up + Vector3Int.back);
        public VoxelTile forwardleft => grid.GetTileAtIndex(position + Vector3Int.forward + Vector3Int.left);
        public VoxelTile forwardright => grid.GetTileAtIndex(position + Vector3Int.forward + Vector3Int.right);
        public VoxelTile backleft => grid.GetTileAtIndex(position + Vector3Int.back + Vector3Int.left);
        public VoxelTile backright => grid.GetTileAtIndex(position + Vector3Int.back + Vector3Int.right);
        public VoxelTile downleft => grid.GetTileAtIndex(position + Vector3Int.down + Vector3Int.left);
        public VoxelTile downright => grid.GetTileAtIndex(position + Vector3Int.down + Vector3Int.right);
        public VoxelTile downforward => grid.GetTileAtIndex(position + Vector3Int.down + Vector3Int.forward);
        public VoxelTile downback => grid.GetTileAtIndex(position + Vector3Int.down + Vector3Int.back);
        public VoxelTile upforwardleft => grid.GetTileAtIndex(position + Vector3Int.up + Vector3Int.forward + Vector3Int.left);
        public VoxelTile upforwardright => grid.GetTileAtIndex(position + Vector3Int.up + Vector3Int.forward + Vector3Int.right);
        public VoxelTile downbackleft => grid.GetTileAtIndex(position + Vector3Int.down + Vector3Int.back + Vector3Int.left);
        public VoxelTile downbackright => grid.GetTileAtIndex(position + Vector3Int.down + Vector3Int.back + Vector3Int.right);
        public VoxelTile downforwardleft => grid.GetTileAtIndex(position + Vector3Int.down + Vector3Int.forward + Vector3Int.left);
        public VoxelTile downforwardright => grid.GetTileAtIndex(position + Vector3Int.down + Vector3Int.forward + Vector3Int.right);
        public VoxelTile upbackleft => grid.GetTileAtIndex(position + Vector3Int.up + Vector3Int.back + Vector3Int.left);
        public VoxelTile upbackright => grid.GetTileAtIndex(position + Vector3Int.up + Vector3Int.back + Vector3Int.right);
        public IEnumerable<VoxelTile> AllNeighbors => new List<VoxelTile>{
        upforward,      upforwardright,     upright,    upbackright,    upback,     upbackleft,     upleft,     upforwardleft, up,
        forward,        forwardright,       right,      backright,      back,       backleft,       left,       forwardleft,
        downforward,    downforwardright,   downright,  downbackright,  downback,   downbackleft,   downleft,   downforwardleft, down, };
        public IEnumerable<VoxelTile> AllImmediateNeighbors => new List<VoxelTile>{
            up, down, left, right, forward, back };
        public IEnumerable<VoxelTile> AllDiagonalNeighbors => new List<VoxelTile>{
            upforward, upright, upback, upleft,
            forwardright, backright, backleft, forwardleft,
            downforward, downright, downback, downleft };
        public IEnumerable<VoxelTile> AllCornerNeighbors => new List<VoxelTile>{
            upforwardright, upbackright, upbackleft, upforwardleft,
            downforwardright, downbackright, downbackleft, downforwardleft};

        public IEnumerable<VoxelTile> Neighbors => AllNeighbors.Where((tile) => tile != null);
        public IEnumerable<VoxelTile> ImmediateNeighbors => AllImmediateNeighbors.Where((tile) => tile != null);
        public IEnumerable<VoxelTile> DiagonalNeighbors => AllDiagonalNeighbors.Where((tile) => tile != null);
        public IEnumerable<VoxelTile> CornerNeighbors => AllCornerNeighbors.Where((tile) => tile != null);

        #endregion
    }
}