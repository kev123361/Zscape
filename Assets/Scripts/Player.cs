using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{

    private bool invincible;
    //tileCoord[0] = row, tileCoord[1] = column
    public int[] playerCoordinates;

    // Start is called before the first frame update
    void Start()
    {
        bm = board.GetComponent<BoardManager>();
        boardSize = bm.tiles.Length;
        playerCoordinates = new int[2];

        //Event handler allows the game to adjust the position of the player AFTER the board spawns in the board
        BoardManager.OnBeginRound += SetStartingPosition;
    }

    private void Update()
    {
        CheckMovementInputs();
    }

    private void CheckMovementInputs()
    {
        //Up one row
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerCoordinates[0] -= 1;
            if (LegalMove(playerCoordinates))
            {
                Move(playerCoordinates);
            }
            else { playerCoordinates[0]++; }   
        }
        //Left one column
        else if (Input.GetKeyDown(KeyCode.A))
        {
            playerCoordinates[1] -= 1;
            if (LegalMove(playerCoordinates))
            {
                Move(playerCoordinates);
            }
            else { playerCoordinates[1]++; }
        }
        //Down one row
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerCoordinates[0] += 1;
            if (LegalMove(playerCoordinates))
            {
                Move(playerCoordinates);
            }
            else { playerCoordinates[0]--; }
        }
        //Right one column
        else if (Input.GetKeyDown(KeyCode.D))
        {
            playerCoordinates[1] += 1;
            if (LegalMove(playerCoordinates))
            {
                Move(playerCoordinates);
            }
            else { playerCoordinates[1]--; }
        }
        
        //Shoot
        else if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }
    }

    public void SetStartingPosition()
    {
        playerCoordinates[0] = bm.currentBoardSize[0] / 2;
        playerCoordinates[1] = bm.currentBoardSize[1] / 2;
        Move(playerCoordinates);
    }

    private void CheckShootInput()
    {
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
    
    new public void LoseHealth(int damage)
    {
        if (!invincible) {
            health -= damage;
            StartCoroutine(InvincibilityFrames());
        }
        
    }

    private IEnumerator InvincibilityFrames()
    {
        invincible = true;

        int flashes = 0;
        MeshRenderer mesh = GetComponent<MeshRenderer>();

        while (flashes < 6)
        {
            mesh.enabled = !mesh.enabled;
            yield return new WaitForSeconds(.1f);
            flashes += 1;
        }

        invincible = false;
    }
}
