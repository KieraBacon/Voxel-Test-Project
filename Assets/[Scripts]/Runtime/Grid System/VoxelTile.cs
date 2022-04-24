using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelTile
{
    public Vector3Int position { get; private set; }
    public GameObject gameObject;
    public VoxelTile(Vector3Int position) { this.position = position; }
}
