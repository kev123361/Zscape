using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float existTime;
    public int damage;

    public int enemyDamage;

    public bool isCrit = false;
    private int playerDamage;

    public bool isEnemyProjectile;
    public bool isIndicatorOn;
    public bool isBeam;

    private float timer = 0f;
    private Tile currentTile;

    private List<Tile> collidingTiles;

    public Rigidbody rb;

    public Projectile(float speed, float acceleration)
    {
        this.speed = speed;
        this.acceleration = acceleration;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, speed);
        collidingTiles = new List<Tile>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > existTime)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * acceleration);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isEnemyProjectile && other.gameObject.CompareTag("Enemy"))
        {
            if (!isBeam)
            {
                Destroy(gameObject);
            }
            other.gameObject.GetComponent<Enemy>().LoseHealth(damage, isCrit);
        } else if (isEnemyProjectile && other.gameObject.CompareTag("Player"))
        {
            if (!isBeam) {
                Destroy(gameObject);
                if (currentTile)
                {
                    currentTile.UnwarnTile();
                }
            }
            other.GetComponent<Player>().LoseHealth(enemyDamage);

        } else if (!GetComponent<Explosion>() && isEnemyProjectile && isIndicatorOn && other.gameObject.CompareTag("Tile"))
        {
            Tile otherTile = other.GetComponent<Tile>();
            otherTile.WarnTile();
            currentTile = otherTile;
            collidingTiles.Add(otherTile);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (isEnemyProjectile && other.gameObject.CompareTag("Tile"))
        {
            other.gameObject.GetComponent<Tile>().UnwarnTile();
            collidingTiles.Remove(other.GetComponent<Tile>());
        }
    }


    public void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }

    public void SetAcceleration(float newAccel)
    {
        this.acceleration = newAccel;
    }

    public void SetExistTime(float time)
    {
        this.existTime = time;
    }

    public void SetPlayerDamage(int dmg)
    {
        if (!isCrit)
        {
            this.damage = dmg;
        } else
        {
            this.damage = 2 * dmg;
        }
    }

    public void UnwarnCollidingTiles()
    {
        foreach (Tile tile in collidingTiles)
        {
            tile.UnwarnTile();
        }
    }

    public void SetEnemyDamage(float multiplier, int level)
    {
        enemyDamage += Mathf.RoundToInt(enemyDamage * (multiplier * level));
    }
}
