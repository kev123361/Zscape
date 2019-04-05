using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : Enemy
{
    private float timer = 0f;
    private Rigidbody rb;
    private Vector3 sourcePos;
    private bool charging;
    private Tile currentTile;

    new public static event EnemyDeath OnEnemyDeath;
    public float timeToCharge;
    public int damage;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sourcePos = rb.position;
        charging = false;
        timeToCharge = Random.Range(4.0f, 6.0f);
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
        if (timer >= timeToCharge)
        {
           Charge();
           timer = 0f;
           timeToCharge = Random.Range(4.0f, 6.0f);
        }
        if (rb.position.z < -20)
        {
            charging = false;
            rb.position = new Vector3(rb.position.x, rb.position.y, 20);         
        } if (Vector3.Distance(sourcePos, rb.position) < .5 && !charging)
        {
            rb.velocity = Vector3.zero;
            rb.position = sourcePos;
        }
    }

    private void Charge()
    {
        charging = true;
        rb.velocity = new Vector3(0, 0, -1 * speed);
    }

    // Before deleting gameObject, make call to enemy manager to decrement enemy count
    public override void Die()
    {
        base.Die();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().LoseHealth(damage);
        } else if (other.gameObject.CompareTag("Tile") && charging)
        {
            other.gameObject.GetComponent<Tile>().WarnTile();
            currentTile = other.gameObject.GetComponent<Tile>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            other.gameObject.GetComponent<Tile>().UnwarnTile();
        }
    }
}
