using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public GameObject board;
    public BoardManager bm;
    public int boardSize;
    public int pos;
    public float health;

    public Projectile bullet;
    

    // Helper method to check if a move can be made
    public bool LegalMove(int tile)
    {
        if (tile < 0 || tile >= boardSize)
        {
            Debug.Log("Illegal Move attempted to tile #" + tile);
            return false;
        }
        if (tile == pos - 1 && pos % (boardSize / 3) == 0)
        {
            Debug.Log("Illegal Move attempted off the left side of board");
            return false;
        }
        if (tile == pos + 1 && pos % (boardSize / 3) == 7)
        {
            Debug.Log("Illegal Move attempted off the right side of board");
            return false;
        }


        return bm.IsTileOpen(tile);

    }

    // Moves the unit to specified tile
    // Does not check to see if the move is legal, so check must be made beforehand
    public void Move(int tile)
    {
        //gameObject.transform.position = bm.GetTile(tile).transform.position + new Vector3(0, 1f, 0);


        //Vector3 tempPos = transform.position;
        //if (direction == "Left") { transform.position = tempPos - new Vector3(0, 0, 2.5f); }
        //else if (direction == "Right") { transform.position = tempPos + new Vector3(0, 0, 2.5f); }
        //else if (direction == "Down") { transform.position = tempPos + new Vector3(2.5f, 0, 0); }
        //else if (direction == "Up") { transform.position = tempPos - new Vector3(2.5f, 0, 0); }
    }

    public void LoseHealth(float damage)
    {
        health -= damage;
    }

    public void Shoot()
    {

    }
}
