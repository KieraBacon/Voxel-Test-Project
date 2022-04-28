using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace VoxelGridSystem
{
    public class TileBase : MonoBehaviour
    {
        private static TileBase defaultTilePrefab = null;
        public static TileBase DefaultTilePrefab
        {
            get
            {
                if (!defaultTilePrefab)
                    defaultTilePrefab = Resources.Load<TileBase>("Voxel Tile");
                return defaultTilePrefab;
            }
        }

        [SerializeField]
        private TileWorldObject worldObject;
        [SerializeField]
        private TileSelectionMesh selectionMesh;
        [SerializeField]
        private TileData tileData;
        public TileGrid grid { get; private set; }
        public Vector3Int? position { get; private set; }

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
            get => selectionMesh.gameObject.activeInHierarchy;
            set => selectionMesh.gameObject.SetActive(value);
        }

        public TileData TileData
        {
            get => tileData;
            set
            {
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
        public TileBase Up                 => GetNeighbor(Vector3Int.up);
        public TileBase Down               => GetNeighbor(Vector3Int.down);
        public TileBase Left               => GetNeighbor(Vector3Int.left);
        public TileBase Right              => GetNeighbor(Vector3Int.right);
        public TileBase Forward            => GetNeighbor(Vector3Int.forward);
        public TileBase Back               => GetNeighbor(Vector3Int.back);
        public TileBase UpLeft             => GetNeighbor(Vector3Int.up + Vector3Int.left);
        public TileBase UpRight            => GetNeighbor(Vector3Int.up + Vector3Int.right);
        public TileBase UpForward          => GetNeighbor(Vector3Int.up + Vector3Int.forward);
        public TileBase UpBack             => GetNeighbor(Vector3Int.up + Vector3Int.back);
        public TileBase ForwardLeft        => GetNeighbor(Vector3Int.forward + Vector3Int.left);
        public TileBase ForwardRight       => GetNeighbor(Vector3Int.forward + Vector3Int.right);
        public TileBase BackLeft           => GetNeighbor(Vector3Int.back + Vector3Int.left);
        public TileBase BackRight          => GetNeighbor(Vector3Int.back + Vector3Int.right);
        public TileBase DownLeft           => GetNeighbor(Vector3Int.down + Vector3Int.left);
        public TileBase DownRight          => GetNeighbor(Vector3Int.down + Vector3Int.right);
        public TileBase DownForward        => GetNeighbor(Vector3Int.down + Vector3Int.forward);
        public TileBase DownBack           => GetNeighbor(Vector3Int.down + Vector3Int.back);
        public TileBase UpForwardLeft      => GetNeighbor(Vector3Int.up + Vector3Int.forward + Vector3Int.left);
        public TileBase UpForwardRight     => GetNeighbor(Vector3Int.up + Vector3Int.forward + Vector3Int.right);
        public TileBase DownBackLeft       => GetNeighbor(Vector3Int.down + Vector3Int.back + Vector3Int.left);
        public TileBase DownBackRight      => GetNeighbor(Vector3Int.down + Vector3Int.back + Vector3Int.right);
        public TileBase DownForwardLeft    => GetNeighbor(Vector3Int.down + Vector3Int.forward + Vector3Int.left);
        public TileBase DownForwardRight   => GetNeighbor(Vector3Int.down + Vector3Int.forward + Vector3Int.right);
        public TileBase UpBackLeft         => GetNeighbor(Vector3Int.up + Vector3Int.back + Vector3Int.left);
        public TileBase UpBackRight        => GetNeighbor(Vector3Int.up + Vector3Int.back + Vector3Int.right);
        public ReadOnlyCollection<TileBase> AllNeighbors => new List<TileBase>{
            UpForwardLeft,      UpForward,      UpBackRight,
            UpLeft,             Up,             UpRight,
            UpBackLeft,         UpBack,         UpBackRight,

            ForwardLeft,        Forward,        ForwardRight,
            Left,                               Right,
            BackLeft,           Back,           BackRight,

            DownForwardLeft,    DownForward,    DownBackRight,
            DownLeft,           Down,           DownRight,
            DownBackLeft,       DownBack,       DownBackRight,
        }.AsReadOnly();
        public ReadOnlyCollection<TileBase> AllImmediateNeighbors => new List<TileBase>{
            Up, Forward, Left, Right, Back, Down,
        }.AsReadOnly();
        public ReadOnlyCollection<TileBase> AllDiagonalNeighbors => new List<TileBase>{
            UpForward, UpLeft, UpRight, UpBack,
            ForwardLeft, ForwardRight, BackLeft, BackRight,
            DownForward, DownLeft, DownRight, DownBack,
        }.AsReadOnly();
        public ReadOnlyCollection<TileBase> AllCornerNeighbors => new List<TileBase>{
            UpForwardLeft, UpForwardRight, UpBackLeft, UpBackRight,
            DownBackLeft, DownForwardRight, DownForwardLeft, DownBackRight,
        }.AsReadOnly();

        public ReadOnlyCollection<TileBase> Neighbors => AllNeighbors.Where((tile) => tile != null).ToList().AsReadOnly();
        public ReadOnlyCollection<TileBase> ImmediateNeighbors => AllImmediateNeighbors.Where((tile) => tile != null).ToList().AsReadOnly();
        public ReadOnlyCollection<TileBase> CloseNeighbors => AllImmediateNeighbors.Concat(AllDiagonalNeighbors).Where((tile) => tile != null).ToList().AsReadOnly();
        public ReadOnlyCollection<TileBase> DiagonalNeighbors => AllDiagonalNeighbors.Where((tile) => tile != null).ToList().AsReadOnly();
        public ReadOnlyCollection<TileBase> NonImmediateNeighbors => AllDiagonalNeighbors.Concat(AllCornerNeighbors).Where((tile) => tile != null).ToList().AsReadOnly();
        public ReadOnlyCollection<TileBase> CornerNeighbors => AllCornerNeighbors.Where((tile) => tile != null).ToList().AsReadOnly();
        #endregion
    }
}