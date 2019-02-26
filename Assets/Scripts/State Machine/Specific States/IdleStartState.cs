using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We use this header b/c the class was made W/O inheriting from monobehavior, so just C#. This allows the class to be visible in the inspector
[System.Serializable]
public class IdleStartState : FlowState
{

    public override IEnumerator OnEnter()
    {
        Debug.Log("Enter StartState");

        startButton.onClick.AddListener(StartGameplayLoop);
        //Show/activate UI

        return base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override IEnumerator OnExit()
    {
        Debug.Log("Exit StartState");
        startButton.onClick.RemoveAllListeners();
        return base.OnExit();
    }

    private void StartGameplayLoop()
    {
        Debug.Log("button linked");
        //Logic to begin gameplay loop by calling the board manager

        //Deactivate ALL UI
        startButton.gameObject.SetActive(false);
        //Chagnes the state to the 'PlayState' in the state machine
        StartCoroutine(currentMachine.SwitchState(parentObject.playState));
    }
}
