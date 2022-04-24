using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelProject.GridSystem
{
    public class VoxelGrid : MonoBehaviour
    {
        [SerializeField]
        private VoxelTile tilePrefab;
        public VoxelTile[,,] tiles;
        public Vector3Int Dimensions => new Vector3Int(tiles.GetLength(0), tiles.GetLength(1), tiles.GetLength(2));
        public Vector3 scale;
        [SerializeField]
        private bool centerOnPivot;

        private void Awake()
        {
            SetDimensions(new Vector3Int(16, 16, 16));
        }

        public void SetDimensions(Vector3Int size)
        {
            tiles = new VoxelTile[size.x, size.y, size.z];

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    for (int z = 0; z < size.z; z++)
                    {
                        tiles[x, y, z] = Instantiate(tilePrefab);
                        tiles[x, y, z].Init(this, new Vector3Int(x, y, z));
                        tiles[x, y, z].transform.SetParent(transform);

                        Vector3 position = new Vector3(x * scale.x, y * scale.y, z * scale.z);
                        if (centerOnPivot)
                            position -= new Vector3(size.x * scale.x * 0.5f, size.y * scale.y * 0.5f, size.z * scale.z * 0.5f);
                        tiles[x, y, z].transform.position = position;
                    }
                }
            }
        }

        public VoxelTile GetTileAtIndex(Vector3Int index)
        {
            Vector3Int dimensions = Dimensions;
            if (index.x < 0 || index.x >= dimensions.x ||
                index.y < 0 || index.y >= dimensions.y ||
                index.z < 0 || index.z >= dimensions.z)
                return null;

            return tiles[index.x, index.y, index.z];
        }

        public VoxelTile GetTileAtPosition(Vector3 position)
        {
            if (tiles == null || scale.x * scale.y * scale.z == 0) return null;

            Vector3Int dimensions = Dimensions;
            Vector3 localPos = position -= transform.position - new Vector3(dimensions.x * scale.x * 0.5f, dimensions.y * scale.y * 0.5f, dimensions.z * scale.z * 0.5f);
            Vector3Int intPos = new Vector3Int((int)(localPos.x / scale.x), (int)(localPos.y / scale.y), (int)(localPos.z / scale.z));
            VoxelTile tile = GetTileAtIndex(intPos);
            return tile;
        }

        VoxelTile currentlyHighlightedTile = null;
        private void Update()
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Physics.Raycast(ray, out RaycastHit hitInfo);
            //VoxelTile tile = GetTileAtPosition(hitInfo.point);
            //Debug.DrawLine(Camera.main.transform.position + Vector3.down, hitInfo.point, Color.magenta);
            //if (tile != null)
            //{
            //    if (currentlyHighlightedTile != null)
            //        currentlyHighlightedTile.Highlight = false;
            //    currentlyHighlightedTile = tile;
            //    currentlyHighlightedTile.Highlight = true;
            //    Debug.Log(currentlyHighlightedTile.position + " " + currentlyHighlightedTile.selector.Collider.enabled);
            //
            //    foreach (VoxelTile neighbor in tile.AllNeighbors)
            //    {
            //        if (neighbor != null)
            //        {
            //            if (neighbor.selector.Collider.enabled == true)
            //                Debug.DrawLine(Camera.main.transform.position + Vector3.down, neighbor.transform.position, Color.green);
            //            //else
            //            //    Debug.DrawLine(Camera.main.transform.position + Vector3.down, neighbor.transform.position, Color.red);
            //        }
            //    }
            //    Debug.DrawLine(Camera.main.transform.position + Vector3.down, tile.transform.position, Color.magenta);
            //}
        }

        private void OnDrawGizmos()
        {

        }
    }
}