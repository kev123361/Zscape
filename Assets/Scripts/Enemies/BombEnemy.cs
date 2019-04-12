using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemy
{
    private float timer = 0f;
    private Vector2Int target;

    public int bombDamage;
    public GameObject deathModel;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<UnitAudio>();
        timeToShoot = Random.Range(3.0f, 5.0f);
        //boardSize = bm.tiles.Length;
        EliteSpawn();
        SetLevelStats();
        //Dumb way to get the health UI to update
        LoseHealth(0);
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
            timeToShoot = Random.Range(3.0f, 5.0f);
        }
    }

    new private void Shoot()
    {
        StartCoroutine(BombBarrage());
    }

    // Before deleting gameObject, make call to enemy manager to decrement enemy count
    private void Die()
    {
        //GameObject.FindGameObjectWithTag("Enemy Manager").GetComponent<EnemyManager>().EnemyDied();
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
        //audio.PlayDeathSFX();
        Instantiate(deathModel, transform.position, transform.rotation);
        Destroy(gameObject);
        
    }

    private IEnumerator BombBarrage()
    {
        int bombsFired = 0;
        while (bombsFired < 3)
        {
            audio.PlayThrowSFX();
            var newBomb = Instantiate(bomb, transform.position + (transform.up), transform.rotation);
            target.Set(Random.Range(0, 3), Random.Range(0, 4));
            newBomb.SetTarget(target);
            newBomb.SetBoard(bm);
            newBomb.SetEnemyProjectile(true);
            newBomb.SetRigidBody(newBomb.GetComponent<Rigidbody>());
            newBomb.damage = GetBombDamage();
            newBomb.SetVelocity(new Vector3(2 * (target.x - pos.x), 4, 2 * (target.y - pos.y)));
            
            bombsFired += 1;

            yield return new WaitForSeconds(.75f);
        }
    }

    private int GetBombDamage()
    {
        if (bm.level > 2)
        {
            return Mathf.RoundToInt(bombDamage + (bombDamage * difficultyMultiplier * bm.level));
        }
        return Mathf.RoundToInt(bombDamage + (bombDamage * difficultyMultiplier));
    }
}