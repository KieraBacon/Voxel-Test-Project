using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VoxelGridSystem
{
    public class TileBase : MonoBehaviour
    {
        [SerializeField]
        private TileWorldObject worldObject;
        [SerializeField]
        private TileSelectionMesh selectionMesh;
        [SerializeField]
        private TileData tileData;
        public TileGrid grid { get; private set; }
        public Vector3Int? position { get; private set; }

        public bool Filled {
            get => worldObject.gameObject.activeInHierarchy;
            set {
                worldObject.gameObject.SetActive(value);
                Selectable = value;
            }
        }

        public bool Selectable {
            get => selectionMesh.gameObject.activeInHierarchy;
            set => selectionMesh.gameObject.SetActive(value);
        }

        public TileData TileData {
            get => tileData;
            set {
                tileData = value;
                worldObject.MeshFilter.mesh = value.Mesh;
                worldObject.transform.localRotation = value.Rotation;
            }
        }

        private void Awake()
        {
            TileData = tileData;
        }

        private void OnValidate()
        {
            TileData = tileData;
        }

        public void Init(TileGrid grid, Vector3Int position)
        {
            Filled = false;
            Selectable = false;
            this.grid = grid;
            if (this.grid)
            {
                this.position = position;
                transform.SetParent(grid.transform);
            }
            else
            {
                this.position = null;
                transform.SetParent(null);
            }
        }

        public void Reset()
        {
            Filled = false;
            Selectable = false;
            grid = null;
            position = null;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        #region Neighbors
        private TileBase GetNeighbor(Vector3Int offset)
        {
            if (!grid || !position.HasValue) return null;
            return grid.GetTileAtGridPosition(position.Value + offset);
        }
        public TileBase up                 => GetNeighbor(Vector3Int.up);
        public TileBase down               => GetNeighbor(Vector3Int.down);
        public TileBase left               => GetNeighbor(Vector3Int.left);
        public TileBase right              => GetNeighbor(Vector3Int.right);
        public TileBase forward            => GetNeighbor(Vector3Int.forward);
        public TileBase back               => GetNeighbor(Vector3Int.back);
        public TileBase upleft             => GetNeighbor(Vector3Int.up + Vector3Int.left);
        public TileBase upright            => GetNeighbor(Vector3Int.up + Vector3Int.right);
        public TileBase upforward          => GetNeighbor(Vector3Int.up + Vector3Int.forward);
        public TileBase upback             => GetNeighbor(Vector3Int.up + Vector3Int.back);
        public TileBase forwardleft        => GetNeighbor(Vector3Int.forward + Vector3Int.left);
        public TileBase forwardright       => GetNeighbor(Vector3Int.forward + Vector3Int.right);
        public TileBase backleft           => GetNeighbor(Vector3Int.back + Vector3Int.left);
        public TileBase backright          => GetNeighbor(Vector3Int.back + Vector3Int.right);
        public TileBase downleft           => GetNeighbor(Vector3Int.down + Vector3Int.left);
        public TileBase downright          => GetNeighbor(Vector3Int.down + Vector3Int.right);
        public TileBase downforward        => GetNeighbor(Vector3Int.down + Vector3Int.forward);
        public TileBase downback           => GetNeighbor(Vector3Int.down + Vector3Int.back);
        public TileBase upforwardleft      => GetNeighbor(Vector3Int.up + Vector3Int.forward + Vector3Int.left);
        public TileBase upforwardright     => GetNeighbor(Vector3Int.up + Vector3Int.forward + Vector3Int.right);
        public TileBase downbackleft       => GetNeighbor(Vector3Int.down + Vector3Int.back + Vector3Int.left);
        public TileBase downbackright      => GetNeighbor(Vector3Int.down + Vector3Int.back + Vector3Int.right);
        public TileBase downforwardleft    => GetNeighbor(Vector3Int.down + Vector3Int.forward + Vector3Int.left);
        public TileBase downforwardright   => GetNeighbor(Vector3Int.down + Vector3Int.forward + Vector3Int.right);
        public TileBase upbackleft         => GetNeighbor(Vector3Int.up + Vector3Int.back + Vector3Int.left);
        public TileBase upbackright        => GetNeighbor(Vector3Int.up + Vector3Int.back + Vector3Int.right);
        public List<TileBase> AllImmediateNeighbors => new List<TileBase>{
            up, down, left, right, forward, back };
        public List<TileBase> AllDiagonalNeighbors => new List<TileBase>{
            upforward, upright, upback, upleft,
            forwardright, backright, backleft, forwardleft,
            downforward, downright, downback, downleft };
        public List<TileBase> AllCornerNeighbors => new List<TileBase>{
            upforwardright, upbackright, upbackleft, upforwardleft,
            downforwardright, downbackright, downbackleft, downforwardleft};

        public IEnumerable<TileBase> AllNeighbors => new List<TileBase>().Concat(AllImmediateNeighbors).Concat(AllDiagonalNeighbors).Concat(AllCornerNeighbors);
        public IEnumerable<TileBase> Neighbors => AllNeighbors.Where((tile) => tile != null);
        public IEnumerable<TileBase> ImmediateNeighbors => AllImmediateNeighbors.Where((tile) => tile != null);
        public IEnumerable<TileBase> CloseNeighbors => AllImmediateNeighbors.Concat(AllDiagonalNeighbors).Where((tile) => tile != null);
        public IEnumerable<TileBase> DiagonalNeighbors => AllDiagonalNeighbors.Where((tile) => tile != null);
        public IEnumerable<TileBase> NonImmediateNeighbors => AllDiagonalNeighbors.Concat(AllCornerNeighbors).Where((tile) => tile != null);
        public IEnumerable<TileBase> CornerNeighbors => AllCornerNeighbors.Where((tile) => tile != null);
        #endregion
    }
}