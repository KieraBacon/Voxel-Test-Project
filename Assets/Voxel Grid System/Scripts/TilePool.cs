using UnityEngine;
using UnityEngine.Pool;

namespace VoxelGridSystem
{
    public static class TilePool
    {
        #region Object Pooling
        private static ObjectPool<TileBase> pool = new ObjectPool<TileBase>(OnCreate, OnGet, OnRelease, OnDestroy);
        private static TileBase tilePrefab = null;

        public static TileBase OnCreate()
        {
            if (tilePrefab == null)
            {
                tilePrefab = Resources.Load<TileBase>("Voxel Tile");
                Debug.Log("Voxel Tile prefab loaded from resources.");
            }
            TileBase gridSpace = GameObject.Instantiate(tilePrefab);
            gridSpace.gameObject.SetActive(false);
            return gridSpace;
        }

        private static void OnGet(TileBase tile)
        {
            tile.Reset();
        }

        private static void OnRelease(TileBase tile)
        {
            tile.gameObject.SetActive(false);
            tile.transform.SetParent(null);
        }

        private static void OnDestroy(TileBase tile)
        {
            Debug.LogError("Tile " + tile.name + " was destroyed instead of being released to its object pool.");
        }

        public static TileBase Get(TileGrid grid, Vector3Int position)
        {
            TileBase tile = pool.Get();
            tile.Init(grid, position);
            tile.gameObject.SetActive(true);
            return tile;
        }
        #endregion
    }
}
