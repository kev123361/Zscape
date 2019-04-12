using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using System; interferes with System.Collections ffs

[System.Serializable]
public class UpgradeState : FlowState
{
    /*Plausible Upgrades
     *  Player Health
     *  Bullet Damage
     *  Bomb Damage
     *  Bomb CD Reduction
     *  
    */
    private enum UPGRADES
    {
        Health,
        BulletDmg,
        BombDmg,
        BombCD,
        BulletCD,
        Armor,
        Crit
    };
    private string[] descriptions =
    {
        "Restore 50 Health",
        "+5 Bullet Damage",
        "+10 Bomb Damage",
        "+15% Bomb Fire Rate",
        "+25% Bullet Fire Rate",
        "+5 Armor",
        "+15% Critical Chance"
    };

    //Turret
    //Glass Cannon
    //Lifesteal

    private string[] rareDescriptions =
    {
        "Fire faster when stationary",
        "Deal and Receive 2x Damage",
        "+5 Health on Enemy Kills"
    };

    private int numOfUpgrades = 7;
    private int[] randUpgrades = new int[3];

    private int numOfRareUpgrades = 3;

    private float rareUpgradeChance = .2f;
    private bool isRare = false;

    public override IEnumerator OnEnter()
    {
        Debug.Log("Enter UpgradeState");
        isRare = false;
        
        Shuffle();
        StartButtonManager();
        ClearScreen();

        return base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override IEnumerator OnExit()
    {
        Debug.Log("Exit UpgradeState");
        EndButtonManager();

        yield return new WaitForSeconds(2f);
        yield return base.OnExit();
    }

    private void UpgradeStats(int upgradeNum)
    {
        switch (upgradeNum)
        {
            case 0:
                playerHealthUpgrade();
                Debug.Log("health");
                break;
            case 1:
                bulletDamageUpgrade();
                Debug.Log("bullet dmg");
                break;
            case 2:
                bombDamageUpgrade();
                Debug.Log("bombdmg");
                break;
            case 3:
                bombCDUpgrade();
                Debug.Log("cd");
                break;
            case 4:
                bulletCDUpgrade();
                break;
            case 5:
                armorUpgrade();
                break;
            case 6:
                critUpgrade();
                break;
            default:
                break;
        }

        //Send the state machine back to the Play State since this means the player survived
        StartCoroutine(currentMachine.SwitchState(parentObject.playState));
    }

    private void RareUpgrade(int upgradeNum)
    {
        switch(upgradeNum)
        {
            case 0:
                turretUpgrade();
                break;
            case 1:
                glassCannon();
                break;
            case 2:
                lifesteal();
                break;
            default:
                break;
           
        }
        //Send the state machine back to the Play State since this means the player survived
        StartCoroutine(currentMachine.SwitchState(parentObject.playState));
    }

    private void ClearScreen()
    {
        playerRef.gameObject.SetActive(false);
        enemyManagerRef.gameObject.SetActive(false);
        boardManagerRef.gameObject.SetActive(false);
    }

    #region Plausible Upgrades/Managers
    private void Shuffle()
    {
        
        Random.InitState(System.DateTime.Now.Millisecond);
        int[] numberList = new int[numOfUpgrades];

        for(int j = 0; j < numOfUpgrades; j++)
        {
            numberList[j] = j;
        }

        for(int i = 0; i < randUpgrades.Length; i++)
        {
            int rand = Random.Range(0, numberList.Length - i - 1);
            randUpgrades[i] = numberList[rand];
            numberList[rand] = numberList[numberList.Length - i - 1];
            Debug.Log(i + "trial. Value: " + randUpgrades[i]);
        }

        // If a rare upgrade procs, replace the first upgrade with a random rare upgrade
        if (Random.Range(0f, 1f) < rareUpgradeChance) {
            isRare = true;
            randUpgrades[0] = Random.Range(0, numOfRareUpgrades);
        }
    }

    private void playerHealthUpgrade()
    {
        //Restore 50 Health
        int upgradeInt = 50;
        playerRef.UpgradeHealth(upgradeInt);
    }

    private void bulletDamageUpgrade()
    {
        //Bullet Damage +5
        int upgradeInt = 5;
        playerRef.UpgradeBulletDamage(upgradeInt);
    }

    private void bombDamageUpgrade()
    {
        //Bomb Damage +10
        int upgradeInt = 10;
        playerRef.UpgradeBombDamage(upgradeInt);
    }

    private void bombCDUpgrade()
    {
        //Bomb Fire Rate +15%
        float fireRateIncrease = .15f;
        playerRef.UpgradeBombCD(fireRateIncrease);
    }

    private void bulletCDUpgrade()
    {
        //Bullet Fire Rate +25%
        float fireRateIncrease = .25f;
        playerRef.UpgradeBulletCD(fireRateIncrease);
    }

    private void armorUpgrade()
    {
        //+5 Armor
        playerRef.upgradeArmor(5);
    }

    private void critUpgrade()
    {
        //+15% Crit Chance
        playerRef.upgradeCrit(.15f);
    }

    #region Rare Upgrades

    private void turretUpgrade()
    {
        //While standing still, gain increased attack speed
        playerRef.upgradeTurret(.15f);
    }

    private void glassCannon()
    {
        //Deal and Receive 2x Damage
        playerRef.upgradeGlassCannon();
    }

    private void lifesteal()
    {
        //+5 Health on Enemy Kill
        playerRef.upgradeLifesteal(5);
    }

    #endregion

    private void StartButtonManager()
    {//Finding a way to make it so you simply
        upgradePanel.gameObject.SetActive(true);

        if (!isRare)
        {
            upgradeButton1.GetComponentInChildren<Text>().text = descriptions[randUpgrades[0]]; //+ " by: " + xyz;
            upgradeButton1.onClick.AddListener(() => UpgradeStats(randUpgrades[0]));

            upgradeButton1.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        } else
        {
            upgradeButton1.GetComponentInChildren<Text>().text = rareDescriptions[randUpgrades[0]];
            upgradeButton1.onClick.AddListener(() => RareUpgrade(randUpgrades[0]));

            upgradeButton1.gameObject.GetComponent<Image>().color = new Color(164 / 255f, 248 / 255f, 81 / 255f, 1);
        }
        upgradeButton2.GetComponentInChildren<Text>().text = descriptions[randUpgrades[1]];
        upgradeButton3.GetComponentInChildren<Text>().text = descriptions[randUpgrades[2]];

        
        upgradeButton2.onClick.AddListener(() => UpgradeStats(randUpgrades[1]));
        upgradeButton3.onClick.AddListener(() => UpgradeStats(randUpgrades[2]));
    }

    private void EndButtonManager()
    {
        upgradePanel.gameObject.GetComponent<Animator>().SetTrigger("slideout");
        //upgradeButton1.gameObject.SetActive(false);
        //upgradeButton2.gameObject.SetActive(false);
        //upgradeButton3.gameObject.SetActive(false);

        upgradeButton1.onClick.RemoveAllListeners();
        upgradeButton2.onClick.RemoveAllListeners();
        upgradeButton3.onClick.RemoveAllListeners();
    }
    #endregion

}
