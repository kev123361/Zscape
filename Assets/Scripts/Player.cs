using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField]
    private bool invincible;
    private Vector2Int target;
    public float bombCD;
    private float initialBombCD;
    private float bombRateModifier = 0f;
    
    public float shootCD;
    private float initialShootCD;
    private float baseShootRateModifier;
    private float shootRateModifier = 0f;
    public int bulletDamage = 10;
    public int bombDamage = 25;
    public float bulletSpeed;
    public float bulletAccel;
    public int armor = 0;
    public float critChance = 0f;

    //rare parameters
    public bool isTurret = false;
    public float turretModifer = 0f;
    public int turretStacks = 0;

    public int lifesteal = 0;

    public int glassCannonStacks = 0;
    

    

    private bool canBomb = true;
    private bool canShoot = true;
    public CooldownIcon shootCooldownIcon;
    public CooldownIcon bombCooldownIcon;

    public GameObject model;
    

    new public UnitAudio audio;

    //tileCoord[0] = row, tileCoord[1] = column
    public int[] playerCoordinates;

    public delegate void DeathEvent();
    public static event DeathEvent OnDeath;

    //new protected int maxHealth = 150;
    //new protected int health;

    

    // Start is called before the first frame update
    void Start()
    {
        bm = board.GetComponent<BoardManager>();
        boardSize = bm.tiles.Length;
        playerCoordinates = new int[2];
        //health = maxHealth;
        bm.setPersistentHealth(health);
        
        audio = GetComponent<UnitAudio>();

        initialShootCD = shootCD;
        initialBombCD = bombCD;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        //Event handler allows the game to adjust the position of the player AFTER the board spawns in the board
        BoardManager.OnBeginRound += SetStartingPosition;
        BoardManager.OnBeginRound += EnableShooting;
        BoardManager.OnBeginRound += ResetFireRate;

        //BoardManager.OnBeginRound += ResetStats;
        Enemy.OnEnemyDeath += lifestealGain;
    }

    public void OnDisable()
    {
        BoardManager.OnBeginRound -= SetStartingPosition;
        BoardManager.OnBeginRound -= EnableShooting;
        BoardManager.OnBeginRound -= ResetFireRate;
        //BoardManager.OnBeginRound -= ResetStats;
        Enemy.OnEnemyDeath -= lifestealGain;
    }

    private void Update()
    {
        CheckMovementInputs();
    }

    public void ResetFireRate()
    {
        shootRateModifier = baseShootRateModifier;
        turretStacks = 0;
        shootCD = initialShootCD / (1 + (1 * shootRateModifier));
        
        //Debug.Log("turret shoot: " + turretShootCD + " shootCD: " + shootCD + " shootRateMod: " + shootRateModifier);
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
                if (isTurret)
                {
                    if (turretStacks < 5)
                    {
                        turretStacks += 1;
                    }
                    shootRateModifier += turretStacks * turretModifer;
                    shootCD = initialShootCD / (1 + (1 * shootRateModifier));
                }
                Shoot();
                audio.PlayShootSFX();
                StartCoroutine(ShootCooldown());
                shootCooldownIcon.StartCooldown(shootCD);
            }
        }

        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (canBomb)
            {
                Bomb();
                StartCoroutine(BombCooldown());
                bombCooldownIcon.StartCooldown(bombCD);
            }
        }
    }

    public override void Move(int[] tileCoords)
    {
        base.Move(tileCoords);
        ResetFireRate();
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
        newBullet.SetSpeed(bulletSpeed);
        newBullet.SetAcceleration(bulletAccel);
        newBullet.SetExistTime(10f);
        newBullet.SetPlayerDamage(bulletDamage * (glassCannonStacks + 1));
        if (Random.Range( 0f, 1f) < critChance)
        {
            newBullet.isCrit = true;
        }
    }
    
    private void Bomb()
    {
        var newBomb = Instantiate(bomb, transform.position + (transform.up * 2), transform.rotation);
        target.Set(pos.x, pos.y + 4);
        newBomb.SetTarget(target);
        newBomb.SetBoard(bm);
        newBomb.SetEnemyProjectile(false);
        newBomb.damage = bombDamage * (glassCannonStacks + 1);
        audio.PlayThrowSFX();
        newBomb.SetRigidBody(newBomb.GetComponent<Rigidbody>());
        newBomb.SetVelocity(new Vector3(2 * (target.x - pos.x), 4, 2 * (target.y - pos.y)));
        
    }

    new public void LoseHealth(int damage)
    {
        int damageTaken = (damage - armor) * (glassCannonStacks + 1);
        if (!invincible) {
            health -= damageTaken;
            if (health <= 0)
            {
                Death();
            }
            StartCoroutine(InvincibilityFrames());
            ChangeUIHealth(damageTaken);
        }
        bm.setPersistentHealth(health);
    }

    public void setHealth(int healthRef)
    {
        health = healthRef;
        ChangeUIHealth(0);
    }

    public int getMaxHealth()
    {
        return maxHealth;
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

    public void ResetStats()
    {
        shootCD = initialShootCD;
        bombCD = initialBombCD;
        bombRateModifier = 0f;
        baseShootRateModifier = 0f;
        shootRateModifier = 0f;
        bulletDamage = 10;
        bombDamage = 25;
        armor = 0;
        critChance = 0f;
        isTurret = false;
        turretModifer = 0f;
        turretStacks = 0;
        lifesteal = 0;
        glassCannonStacks = 0;
    }

    public void EnableShooting()
    {
        // Make it so player can shoot stuff at the start of each round
        canBomb = true;
        canShoot = true;
        invincible = false;
        model.SetActive(true);
    }

    private IEnumerator InvincibilityFrames()
    {
        invincible = true;

        int flashes = 0;
        

        while (flashes < 6)
        {
            model.SetActive(!model.activeSelf);
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
        if ((health + inputH) > maxHealth)
        {
            health = maxHealth;
        } else
        {
            health += inputH;
        }
        
        ChangeUIHealth(0);
        bm.setPersistentHealth(health);
    }

    public void UpgradeBulletDamage(int inputBonus)
    {
        bulletDamage += inputBonus;
    }

    public void UpgradeBulletCD(float percentBonus)
    {
        baseShootRateModifier += percentBonus;
        shootRateModifier = baseShootRateModifier;
        shootCD = initialShootCD / (1 + (1 * shootRateModifier));
    }

    public void UpgradeBombDamage(int inputBonus)
    {
        bombDamage += inputBonus;
    }

    public void UpgradeBombCD(float percentBonus)
    {
        bombRateModifier += percentBonus;
        bombCD = initialBombCD * (initialBombCD / (initialBombCD + (initialBombCD * bombRateModifier)));
        bombCD = initialBombCD / (1 + (1 * bombRateModifier));
    }

    public void upgradeArmor(int armorGain)
    {
        armor += armorGain;
    }

    public void upgradeCrit(float critGain)
    {
        critChance += critGain;
    }

    public void upgradeTurret(float fireRate)
    {
        isTurret = true;
        turretModifer += fireRate;
    }

    public void upgradeLifesteal(int lifestealAmount)
    {
        lifesteal += lifestealAmount;
    }

    //Helper method called when an enemy dies
    private void lifestealGain()
    {
        
        UpgradeHealth(lifesteal);
    }

    public void upgradeGlassCannon()
    {
        glassCannonStacks += 1;
    }


}
