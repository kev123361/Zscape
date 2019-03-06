using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeState : FlowState
{
    public override IEnumerator OnEnter()
    {
        Debug.Log("Enter UpgradeState");
        upgradeButton.gameObject.SetActive(true);
        upgradeButton.onClick.AddListener(UpgradeStats);
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
        upgradeButton.onClick.RemoveAllListeners();
        return base.OnExit();
    }

    private void UpgradeStats()
    {
        upgradeButton.gameObject.SetActive(false);
        //Call the player upgrade function
        playerRef.UpgradeHealth();

        //Send the state machine back to the Play State since this means the player survived
        StartCoroutine(currentMachine.SwitchState(parentObject.playState));
    }

    private void ClearScreen()
    {
        playerRef.gameObject.SetActive(false);
        enemyManagerRef.gameObject.SetActive(false);
        boardManagerRef.gameObject.SetActive(false);
    }

}
