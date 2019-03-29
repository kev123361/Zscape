using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
    private float timer = 0f;
    private int[] adjTile = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        adjTile[1] = pos.y;
        //bm = board.GetComponent<BoardManager>();
        //boardSize = bm.tiles.Length;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Die();
        }
        setMovement();
        timer += Time.deltaTime;
        if (timer >= timeToShoot)
        {
            if (Random.Range(-10, 10) >= -2)
            {
                Shoot();
            } else
            {
                Move(adjTile);
            }
            
            timer = 0f;
        }
    }

    new public virtual void Shoot()
    {
        var newBullet = Instantiate(bullet, transform.position + (transform.forward * 2), transform.rotation);
        newBullet.SetSpeed(bulletSpeed);
        newBullet.SetAcceleration(bulletAccel);
        newBullet.SetExistTime(bulletDespawn);
        newBullet.isIndicatorOn = true;
    }

    // Before deleting gameObject, make call to enemy manager to decrement enemy count
    private void Die()
    {
        //GameObject.FindGameObjectWithTag("Enemy Manager").GetComponent<EnemyManager>().EnemyDied();
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
        bm.GetTile(pos.x, pos.y).UnwarnTile();
        Destroy(gameObject);
    }

    // Move in the x axis only
    private void setMovement()
    {
        if (Random.Range(-10, 10) >= 0)
        {
            adjTile[0] = pos.x + 1;
        } else
        {
            adjTile[0] = pos.x - 1;
        }
        if (adjTile[0] > 2 || adjTile[0] < 0) adjTile[0] = 1;
    }
}
