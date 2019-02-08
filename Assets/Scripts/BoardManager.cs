using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardManager : MonoBehaviour
{
    public BoardData tileBoard;
    public Tile tileReference;

    public GameObject[] tiles;
    public int[] tileOccupancy;

    enum tileSet {THREExEIGHT = 0};
    Vector2Int[] boardRatios = {new Vector2Int(3, 8) };

    void Start()
    {
        //Usage of enums to determine which tile configuration we will use. This should hopefully make it more scalable
        tileSet tileSetRef = tileSet.THREExEIGHT;
        //Keeping a reference to the board manager at all times then a holder variable that can be manipulated freely
        Vector3 selfPosition = transform.position;
        Vector3 newPos = selfPosition;
        //BoardManager will be in the top left and tiles will spawn to the right and below it. For loop gets the x THEN the y
        for (int i = 0; i < boardRatios[(int)tileSetRef].x; i++)
        {
            for (int j = 0; j < boardRatios[(int)tileSetRef].y; j++)
            {
                //Instantiates a tileRef then sets it to the tileBoard
                tileBoard.rows[i].column[j] = Instantiate(tileReference, newPos, Quaternion.identity);
                newPos += new Vector3(0, 0, 2.5f);
            }
            //Sets the position for the next row
            newPos = selfPosition + (new Vector3(2.5f, 0, 0)*(i+1));
        }

        
    }

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
    
    public Tile GetTile(int row, int column)
    {
        try
        {
            return tileBoard.rows[row].column[column];
        } catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }
}
