using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private float timer = 0f;
    public float timeToShoot;

    public UnitAudio audio;
    public float difficultyMultiplier = .2f;

    public float bulletSpeed;
    public float bulletAccel;
    public float bulletDespawn;

    public GameObject deathModel;

    public delegate void EnemyDeath();
    public static EnemyDeath OnEnemyDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<Spinny>())
        {
            timeToShoot = Random.Range(0.8f, 1.5f);
        }
        //bm = board.GetComponent<BoardManager>();
        //boardSize = bm.tiles.Length;
        audio = GetComponent<UnitAudio>();
        SetLevelStats();
        //Dumb way to get the health UI to update
        LoseHealth(0);
    }

    protected virtual void SetLevelStats()
    {
        if (bm.level > 2)
        {
            health += health * (difficultyMultiplier * (bm.GetLevel()));
        }
        
        maxHealth = health;
        
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
        newBullet.SetEnemyDamage(difficultyMultiplier, bm.GetLevel());
        newBullet.SetSpeed(bulletSpeed);
        newBullet.SetAcceleration(bulletAccel);
        newBullet.SetExistTime(bulletDespawn);
        newBullet.transform.parent = transform;
        newBullet.isIndicatorOn = true;
        Debug.Log("Checking for GEAR");
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
        //audio.PlayDeathSFX();
        Instantiate(deathModel, transform.position, transform.rotation);
        bm.GetTile(pos.x, pos.y).UnwarnTile();
        Destroy(gameObject);
    }
}
