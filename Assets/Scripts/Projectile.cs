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

    public bool isDiamond;
    private Vector3 pos;
    private Vector3 axis;
    private float freq;
    private float magnitude;

    protected float timer = 0f;
    protected Tile currentTile;

    protected List<Tile> collidingTiles;

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

        pos = transform.position;
        axis = transform.right;
        freq = Random.Range(2, 6);
        magnitude = Random.Range(1, 4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDiamond)
        {
            DisruptDisplacement();
        }
        timer += Time.deltaTime;
        if (timer > existTime)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(!isDiamond)
        {
            rb.AddForce(transform.forward * acceleration);
        }
        
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

    //REFACTOR INTO IT'S SEPERATE CLASS. ONLY FOR THE DIAMOND ENEMY
    private void DisruptDisplacement()
    {
        
        pos += transform.forward * Time.deltaTime * 4f;
        transform.position = pos + axis * Mathf.Sin(Time.time * freq) * 2.5f;

    }
}
