using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{

    private bool invincible;
    private Vector2Int target;
    public float bombCD;
    public float shootCD;
    private int bulletDamage = 10;

    private bool canBomb = true;
    private bool canShoot = true;
    public CooldownIcon shootCooldownIcon;
    public CooldownIcon bombCooldownIcon;

    //tileCoord[0] = row, tileCoord[1] = column
    public int[] playerCoordinates;

    public delegate void DeathEvent();
    public static event DeathEvent OnDeath;

    protected static int maxHealth = 150;
    protected static int hesalth = maxHealth;

    

    // Start is called before the first frame update
    void Start()
    {
        bm = board.GetComponent<BoardManager>();
        boardSize = bm.tiles.Length;
        playerCoordinates = new int[2];
        
    }

    public override void OnEnable()
    {
        base.OnEnable();
        //Event handler allows the game to adjust the position of the player AFTER the board spawns in the board
        BoardManager.OnBeginRound += SetStartingPosition;
        BoardManager.OnBeginRound += EnableShooting;

    }

    public void OnDisable()
    {
        BoardManager.OnBeginRound -= SetStartingPosition;
        BoardManager.OnBeginRound -= EnableShooting;
    }

    private void Update()
    {
        
        CheckMovementInputs();
    }

    private void CheckMovementInputs()
    {
        //Up one row
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerCoordinates[0] -= 1;
            if (LegalMove(playerCoordinates))
            {
                Move(playerCoordinates);
            }
            else { playerCoordinates[0]++; }   
        }
        //Left one column
        else if (Input.GetKeyDown(KeyCode.A))
        {
            playerCoordinates[1] -= 1;
            if (LegalMove(playerCoordinates))
            {
                Move(playerCoordinates);
            }
            else { playerCoordinates[1]++; }
        }
        //Down one row
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerCoordinates[0] += 1;
            if (LegalMove(playerCoordinates))
            {
                Move(playerCoordinates);
            }
            else { playerCoordinates[0]--; }
        }
        //Right one column
        else if (Input.GetKeyDown(KeyCode.D))
        {
            playerCoordinates[1] += 1;
            if (LegalMove(playerCoordinates))
            {
                Move(playerCoordinates);
            }
            else { playerCoordinates[1]--; }
        }
        
        //Shoot
        else if (Input.GetKeyDown(KeyCode.J))
        {
            if (canShoot)
            {
                Shoot();
                StartCoroutine(ShootCooldown());
                shootCooldownIcon.StartCooldown(shootCD);
            }
        }

        else if (Input.GetKeyDown(KeyCode.L))
        {
            if (canBomb)
            {
                Bomb();
                StartCoroutine(BombCooldown());
                bombCooldownIcon.StartCooldown(bombCD);
            }
        }
    }

    public override bool LegalMove(int[] tileCoords)
    {
        return base.LegalMove(tileCoords) && tileCoords[1] < 4;
    }

    public void SetStartingPosition()
    {
        playerCoordinates[0] = bm.currentBoardSize[0] / 2 ;
        playerCoordinates[1] = bm.currentBoardSize[1] / 4;
        Move(playerCoordinates);
    }

    private void CheckShootInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Shoot");
            Shoot();
        }
    }

    new private void Shoot()
    {
        var newBullet = Instantiate(bullet, transform.position + (transform.forward * 2), transform.rotation);
        newBullet.SetSpeed(0f);
        newBullet.SetAcceleration(10f);
        newBullet.SetExistTime(10f);
        newBullet.SetPlayerDamage(bulletDamage);
    }
    
    private void Bomb()
    {
        var newBomb = Instantiate(bomb, transform.position + (transform.up * 2), transform.rotation);
        target.Set(pos.x, pos.y + 4);
        newBomb.SetTarget(target);
        newBomb.SetBoard(bm);
        newBomb.SetEnemyProjectile(false);
        newBomb.SetRigidBody(newBomb.GetComponent<Rigidbody>());
        newBomb.SetVelocity(new Vector3(2 * (target.x - pos.x), 4, 2 * (target.y - pos.y)));
        
    }

    new public void LoseHealth(int damage)
    {
        if (!invincible) {
            health -= damage;
            if (health <= 0)
            {
                Death();
            }
            StartCoroutine(InvincibilityFrames());
            ChangeUIHealth(damage);
        }
        
    }

    private void ChangeUIHealth(int damage)
    {
        healthScript.UpdateHealth(health, maxHealth, damage);
    }

    private void Death()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }
        //Any other logic perhaps such as clearing the board.
    }

    public void EnableShooting()
    {
        // Make it so player can shoot stuff at the start of each round
        canBomb = true;
        canShoot = true;
    }

    private IEnumerator InvincibilityFrames()
    {
        invincible = true;

        int flashes = 0;
        MeshRenderer mesh = GetComponent<MeshRenderer>();

        while (flashes < 6)
        {
            mesh.enabled = !mesh.enabled;
            yield return new WaitForSeconds(.1f);
            flashes += 1;
        }

        invincible = false;
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCD);
        canShoot = true;
    }

    private IEnumerator BombCooldown()
    {
        canBomb = false;
        yield return new WaitForSeconds(bombCD);
        canBomb = true;
    }

    public void UpgradeHealth(int inputH)
    {
        if ((health += inputH) > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += inputH;
        }
    }

    public void UpgradeBulletDamage(int inputBonus)
    {
        bulletDamage += inputBonus;
    }

    public void UpgradeBombDamage(int inputBonus)
    {
        //no ref yet
    }

    public void UpgradeBombCD(float inputBonus)
    {
        bombCD -= inputBonus;
    }
}
