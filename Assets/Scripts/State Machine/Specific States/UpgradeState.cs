using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeState : FlowState
{
    public override IEnumerator OnEnter()
    {
        Debug.Log("Enter UpgradeState");
        upgradeButton.onClick.AddListener(UpgradeStats);
        upgradeButton.gameObject.SetActive(true);
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
        //playerRef.

        //Send the state machine back to the Play State since this means the player survived
        StartCoroutine(currentMachine.SwitchState(parentObject.playState));
    }
}
