using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
    private float timer = 0f;
    private int[] myTile = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        myTile[0] = pos.x;
        myTile[1] = pos.y;
        audio = GetComponent<UnitAudio>();
        timeToShoot = Random.Range(timeToShoot - .5f, timeToShoot + .5f);
        //bm = board.GetComponent<BoardManager>();
        //boardSize = bm.tiles.Length;
        difficultyMultiplier = .2f;
        EliteSpawn();
        SetLevelStats();
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Die();
        }
        
        timer += Time.deltaTime;
        if (timer >= timeToShoot)
        {
            if (Random.Range(-10, 10) >= -2)
            {
                Shoot();
            } else
            {
                setMovement();
                Move(myTile);
            }
            
            timer = 0f;
            timeToShoot = Random.Range(timeToShoot - .5f, timeToShoot + .5f);
        }
    }

    new public virtual void Shoot()
    {
        var newBullet = Instantiate(bullet, transform.position + (transform.forward * 2), transform.rotation);
        newBullet.SetSpeed(bulletSpeed);
        newBullet.SetAcceleration(bulletAccel);
        newBullet.SetExistTime(bulletDespawn);
        newBullet.isIndicatorOn = true;

        newBullet.SetEnemyDamage(difficultyMultiplier, bm.GetLevel());
    }

    // Before deleting gameObject, make call to enemy manager to decrement enemy count
    private void Die()
    {
        //GameObject.FindGameObjectWithTag("Enemy Manager").GetComponent<EnemyManager>().EnemyDied();
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
        audio.PlayDeathSFX();
        bm.GetTile(pos.x, pos.y).UnwarnTile();
        Destroy(gameObject);
    }

    // Move in the x axis only
    private void setMovement()
    {
        switch (Random.Range(1, 5))
        {
            case 1: //move down
                if (bm.checkUpGridAvailibility(myTile[0], myTile[1], pos.x + 1, myTile[1]))
                {
                    myTile[0] = pos.x + 1;
                }                
                break;
            case 2: //move up
                if (bm.checkUpGridAvailibility(myTile[0], myTile[1], pos.x - 1, myTile[1]))
                {
                    myTile[0] = pos.x - 1;
                }
                break;
            case 3: //move side
                if (bm.checkUpGridAvailibility(myTile[0], myTile[1], myTile[0], pos.y + 1))
                {
                    myTile[1] = pos.y + 1;
                }
                break;
            case 4: //move up
                if (bm.checkUpGridAvailibility(myTile[0], myTile[1], myTile[0], pos.y - 1))
                {
                    myTile[1] = pos.y - 1;
                }
                break;
        }
                
        if (myTile[0] > 2 || myTile[0] < 0) myTile[0] = 1;
        if (myTile[1] > 8) myTile[1] = 7;
        if (myTile[1] < 5) myTile[1] = 6;
    }
}
