using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestTileScript : MonoBehaviour
{
    CheckTile _checkTile;
    Tilemap _tilemap;
    public TileBase _tileBase;

    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();
        _tilemap.SetTile(new Vector3Int(11, -1, 0), _tileBase);
        //LogTile();
    }
    public void LogTile()
    {
        Debug.Log(_tilemap.GetTile(new Vector3Int(-1, 13, 0)).name);
    }
}
