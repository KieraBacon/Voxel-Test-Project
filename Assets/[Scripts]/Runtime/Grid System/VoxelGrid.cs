using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;
    private VoxelTile[,,] tiles;
    public Vector3Int Dimensions => new Vector3Int(tiles.GetLength(0), tiles.GetLength(1), tiles.GetLength(2));
    public Vector3 scale;

    private void Start()
    {
        SetDimensions(new Vector3Int(16, 16, 16));
        Generate();
    }

    public void Generate()
    {
        foreach (VoxelTile tile in tiles)
        {
            Vector3 position = new Vector3(
                tile.position.x * scale.x,
                tile.position.y * scale.y,
                tile.position.z * scale.z);

            GameObject go = Instantiate(tilePrefab, transform);
            go.transform.position = position;
            tile.gameObject = go;
            tile.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
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
                    tiles[x, y, z] = new VoxelTile(this, new Vector3Int(x,y,z));
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

        Vector3 localPos = position -= transform.position;
        Vector3Int intPos = new Vector3Int((int)(localPos.x / scale.x), (int)(localPos.y / scale.y), (int)(localPos.z / scale.z));
        //Debug.DrawLine(Camera.main.transform.position + Vector3.down, intPos, Color.magenta);

        VoxelTile tile = GetTileAtIndex(intPos);
        return tile;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo);
        VoxelTile tile = GetTileAtPosition(hitInfo.point);
        if (tile != null)
        {
            tile.gameObject.GetComponent<MeshRenderer>().enabled = true;
            Debug.DrawLine(Camera.main.transform.position + Vector3.down, tile.position, Color.red);
            foreach (VoxelTile neighbor in tile.neighbors)
            {
                if (neighbor != null)
                {
                    Debug.Log(neighbor == null ? "null" : neighbor.position);
                    Vector3Int dimensions = Dimensions;
                    //Debug.DrawLine(Camera.main.transform.position + Vector3.down, neighbor.position, Color.green );
                }
            }
            Debug.Log(tile.neighbors[0]);
        }
    }

    private void OnDrawGizmos()
    {

    }
}
