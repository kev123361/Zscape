using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private float timer = 0f;
    public float timeToShoot;

    public float bulletSpeed;
    public float bulletAccel;
    public float bulletDespawn;

    public delegate void EnemyDeath();
    public static EnemyDeath OnEnemyDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        timeToShoot = Random.Range(0.8f, 1.5f);
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
        timer += Time.deltaTime;
        if (timer >= timeToShoot)
        {
            Shoot();
            timer = 0f;
            timeToShoot = Random.Range(0.8f, 1.5f);
        }
    }

    new public virtual void Shoot()
    {
        var newBullet = Instantiate(bullet, transform.position + (transform.forward * 2), transform.rotation);
        newBullet.SetSpeed(bulletSpeed);
        newBullet.SetAcceleration(bulletAccel);
        newBullet.SetExistTime(bulletDespawn);
        newBullet.transform.parent = transform;
        newBullet.isIndicatorOn = true;
    }

    // Before deleting gameObject, make call to enemy manager to decrement enemy count
    public virtual void Die()
    {
        //GameObject.FindGameObjectWithTag("Enemy Manager").GetComponent<EnemyManager>().EnemyDied();
        if(OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
        foreach (Projectile projectile in GetComponentsInChildren<Projectile>())
        {
            projectile.UnwarnCollidingTiles();
        }
        bm.GetTile(pos.x, pos.y).UnwarnTile();
        Destroy(gameObject);
    }
}
