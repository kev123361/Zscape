using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public BoardData test;
    public GameObject tileReference;
    public GameObject[] tiles;
    public int[] tileOccupancy;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check tileOccupancy to see if a Tile is open
    // true if open, false if occupied
    public bool IsTileOpen(int tile)
    {
        return true;
    }

    // Tiles ordered from top left to bottom right (0-23)
    // return specified Tile from tiles array
    public GameObject GetTile(int tile)
    {
        if (tile < 0 || tile > 23) {
            Debug.Log("Attempted to retrieve illegal tile: #" + tile);
            return null;
        }
        return tiles[tile];
    }
}
