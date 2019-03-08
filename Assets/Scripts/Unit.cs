using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public GameObject board;
    public BoardManager bm;
    public int boardSize;
    public Vector2Int pos;
    public float health;

    public Projectile bullet;
    public Bomb bomb;
    

    // Helper method to check if a move can be made
    public virtual bool LegalMove(int[] tileCoord)
    {
        //This is a mouthful but essentially checks the tile board to see if the give position
        if (0 <= tileCoord[0] && bm.currentBoardSize[0] > tileCoord[0] &&
            0 <= tileCoord[1] && bm.currentBoardSize[1] > tileCoord[1])
        {
            return bm.IsTileOpen(tileCoord);
        }
        else
        {
            Debug.Log("Illegal Move attempted to tile #" + tileCoord[0] + ", " + tileCoord[1]);
            return false;
        }

    }

    // Moves the unit to specified tile
    // Does not check to see if the move is legal, so check must be made beforehand
    public void Move(int[] tileCoords)
    {
        Vector3 newPos = bm.GetTile(tileCoords[0], tileCoords[1]).transform.position + new Vector3(0, 1f, 0);
        gameObject.transform.position = newPos;
        pos = new Vector2Int(tileCoords[0], tileCoords[1]);
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
    }

    public void Shoot()
    {

    }
}
