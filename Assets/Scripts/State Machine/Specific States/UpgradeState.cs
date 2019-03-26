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
        BombCD
    };
    private int numOfUpgrades = 4;
    private int[] randUpgrades = new int[3];

    public override IEnumerator OnEnter()
    {
        Debug.Log("Enter UpgradeState");
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
        
        return base.OnExit();
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
        //Deprecated way for a 'true' random
        Random.seed = System.DateTime.Now.Millisecond;
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
    }

    private void playerHealthUpgrade()
    {
        int upgradeInt = Random.Range(25, 100);
        playerRef.UpgradeHealth(upgradeInt);
    }

    private void bulletDamageUpgrade()
    {
        int upgradeInt = Random.Range(5, 10);
        playerRef.UpgradeBulletDamage(upgradeInt);
    }

    private void bombDamageUpgrade()
    {
        int upgradeInt = Random.Range(5, 15);
        //NO BOMB DAMAGE REF YET
        Debug.Log("bmbdmg inc");
    }

    private void bombCDUpgrade()
    {
        float upgradeFloat = Random.Range(.2f, .6f);
        playerRef.UpgradeBombCD(upgradeFloat);
    }

    private void StartButtonManager()
    {//Finding a way to make it so you simply
        upgradeButton1.gameObject.SetActive(true);
        upgradeButton2.gameObject.SetActive(true);
        upgradeButton3.gameObject.SetActive(true);
        upgradeButton1.GetComponentInChildren<Text>().text = "Upgrade " + ((UPGRADES)(randUpgrades[0])); //+ " by: " + xyz;
        upgradeButton2.GetComponentInChildren<Text>().text = "Upgrade " + ((UPGRADES)(randUpgrades[1]));
        upgradeButton3.GetComponentInChildren<Text>().text = "Upgrade " + ((UPGRADES)(randUpgrades[2]));

        upgradeButton1.onClick.AddListener(() => UpgradeStats(randUpgrades[0]));
        upgradeButton2.onClick.AddListener(() => UpgradeStats(randUpgrades[1]));
        upgradeButton3.onClick.AddListener(() => UpgradeStats(randUpgrades[2]));
    }

    private void EndButtonManager()
    {
        upgradeButton1.gameObject.SetActive(false);
        upgradeButton2.gameObject.SetActive(false);
        upgradeButton3.gameObject.SetActive(false);

        upgradeButton1.onClick.RemoveAllListeners();
        upgradeButton2.onClick.RemoveAllListeners();
        upgradeButton3.onClick.RemoveAllListeners();
    }
    #endregion

}
