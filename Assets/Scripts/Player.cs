using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{

    // Start is called before the first frame update
    void Start()
    {
        bm = board.GetComponent<BoardManager>();
        boardSize = bm.tiles.Length;
        gameObject.transform.position = bm.GetTile(pos).transform.position + new Vector3(0, 1f, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
            if (LegalMove(pos - 8))
            {
                pos -= 8;
                Move(pos);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A");
            if (LegalMove(pos - 1))
            {
                pos -= 1;
                Move(pos);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S");
            if (LegalMove(pos + 8))
            {
                pos += 8;
                Move(pos);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D");
            if (LegalMove(pos + 1))
            {
                pos += 1;
                Move(pos);
            }
        }
    }

    
    
}
