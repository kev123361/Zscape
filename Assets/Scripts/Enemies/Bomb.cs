﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Vector2Int explosionPos;
    public BoardManager bm;
    public GameObject explosion;
    public bool isEnemyProjectile;
    public int damage;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        bm.GetTile(explosionPos.x, explosionPos.y).WarnBombTile();
    }

    // Update is called once per frame
    void Update()
    {
        bm.GetTile(explosionPos.x, explosionPos.y).WarnBombTile();
        if (rb.position.y < -10) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            GameObject newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            newExplosion.GetComponent<Explosion>().explosionPos = explosionPos;
            newExplosion.GetComponent<Explosion>().bm = bm;
            newExplosion.GetComponent<Projectile>().isEnemyProjectile = isEnemyProjectile;
            newExplosion.GetComponent<Projectile>().damage = damage;
            Destroy(gameObject);
        }
    }

    public void SetTarget(Vector2Int newExplosionPos)
    {
        explosionPos = newExplosionPos;
    }

    public void SetBoard(BoardManager newBM)
    {
        bm = newBM;
    }

    public void SetRigidBody(Rigidbody newRB)
    {
        rb = newRB;
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    public void SetEnemyProjectile(bool enemyProjectile)
    {
        isEnemyProjectile = enemyProjectile;
    }
}
