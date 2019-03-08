using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemy
{
    private float timer = 0f;
    private Vector2Int target;
    public Bomb bomb;
    new public static event EnemyDeath OnEnemyDeath;

    // Start is called before the first frame update
    void Start()
    {
        bm = board.GetComponent<BoardManager>();

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
        }
    }

    new private void Shoot()
    {
        var newBomb = Instantiate(bomb, transform.position + (transform.up * 2), transform.rotation);
        target.Set(pos.x, pos.y - 4);
        newBomb.SetTarget(target);
        newBomb.SetBoard(bm);
        newBomb.SetRigidBody(newBomb.GetComponent<Rigidbody>());
        newBomb.SetVelocity(new Vector3(2*(target.x-pos.x), 4, 2*(target.y-pos.y)));
    }

    // Before deleting gameObject, make call to enemy manager to decrement enemy count
    private void Die()
    {
        //GameObject.FindGameObjectWithTag("Enemy Manager").GetComponent<EnemyManager>().EnemyDied();
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
        Destroy(gameObject);
    }
}