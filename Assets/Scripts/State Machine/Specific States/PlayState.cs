using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayState : FlowState
{
    public override IEnumerator OnEnter()
    {
        Debug.Log("Enter PlayState");
        boardManagerRef.StartBattle();

        //Create an event listener for death status
        //Create an event listener for round win status
        return base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override IEnumerator OnExit()
    {
        Debug.Log("Exit PlayState");
        return base.OnExit();
    }

    private void PlayerDeath()
    {
        //Play any animations whatever
        StartCoroutine(currentMachine.SwitchState(parentObject.endState));
    }

    private void RoundComplete()
    {
        StartCoroutine(currentMachine.SwitchState(parentObject.upgradeState));
    }
}
