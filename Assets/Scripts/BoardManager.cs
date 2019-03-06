using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardManager : MonoBehaviour
{
    public BoardData tileBoard;
    public Tile friendlyBaseTile;
    public Tile enemyBaseTile;

    public GameObject[] tiles;
    public int[] tileOccupancy;

    enum tileSet {THREExEIGHT = 0};
    private Vector2Int[] boardRatios = {new Vector2Int(3, 8) };
    public int[] currentBoardSize;

    public Player playerRef;

    //Event handlers that signify that the board has completed initializing
    public delegate void BeginRound();
    public static event BeginRound OnBeginRound;

    void Start()
    {
        
       
    }

    public void StartBattle()
    {
        //For some reason, arrays need to be initialized at runtime
        currentBoardSize = new int[2];
        //Usage of enums to determine which tile configuration we will use. This should hopefully make it more scalable
        tileSet tileSetRef = tileSet.THREExEIGHT;
        //Keeping a reference to the board manager at all times then a holder variable that can be manipulated freely
        Vector3 selfPosition = transform.position;
        Vector3 newPos = selfPosition;
        //Setting the current size of the board
        currentBoardSize[0] = boardRatios[(int)tileSetRef].x;
        currentBoardSize[1] = boardRatios[(int)tileSetRef].y;
        //BoardManager will be in the top left and tiles will spawn to the right and below it. For loop gets the x THEN the y
        for (int i = 0; i < currentBoardSize[0]; i++)
        {
            for (int j = 0; j < currentBoardSize[1]; j++)
            {
                //Instantiates a tileRef then sets it to the tileBoard
                if (j < 4)
                {
                    tileBoard.rows[i].column[j] = Instantiate(friendlyBaseTile, newPos, Quaternion.identity);
                } else
                {
                    tileBoard.rows[i].column[j] = Instantiate(enemyBaseTile, newPos, Quaternion.identity);
                }
                tileBoard.rows[i].column[j].transform.parent = transform;
                tileBoard.rows[i].column[j].pos = new Vector2Int(i, j);
                newPos += new Vector3(0, 0, 2.5f);
            }
            //Sets the position for the next row
            newPos = selfPosition + (new Vector3(2.5f, 0, 0) * (i + 1));
        }

        if (OnBeginRound != null)
        {
            OnBeginRound();
        }
    }

    void Update()
    {
        
    }

    // Check tileOccupancy to see if a Tile is open
    // true if open, false if occupied
    public bool IsTileOpen(int[] tileCoord)
    {
        return true;
    }

    
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
