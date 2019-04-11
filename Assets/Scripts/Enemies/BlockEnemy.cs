public class BlockEnemy : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<UnitAudio>();
        //bm = board.GetComponent<BoardManager>();
        //boardSize = bm.tiles.Length;
        difficultyMultiplier = .1f;
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
        //audio.PlayDeathSFX();
        Instantiate(deathModel, transform.position, transform.rotation);
        bm.GetTile(pos.x, pos.y).UnwarnTile();
        Destroy(gameObject);
    }
}
