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
        //gameObject.transform.position = bm.GetTile(pos).transform.position + new Vector3(0, 1f, 0);
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
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Shoot");
            Shoot();
        }
    }

    new private void Shoot()
    {
        var newBullet = Instantiate(bullet, transform.position + (transform.forward * 2), transform.rotation);
        newBullet.SetSpeed(0f);
        newBullet.SetAcceleration(10f);
        newBullet.SetExistTime(10f);
    }
    
}
