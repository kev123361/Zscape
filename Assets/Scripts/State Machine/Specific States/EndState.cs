using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EndState : FlowState
{
    public override IEnumerator OnEnter()
    {
        Debug.Log("Enter EndState");
        ButtonSetup();
        ClearScreen();

        //Telemetrics

        return base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override IEnumerator OnExit()
    {
        Debug.Log("Exit EndState");
        endButton.gameObject.SetActive(false);
        endButton.onClick.RemoveAllListeners();
        return base.OnExit();
    }

    private void ButtonSetup()
    {
        endButton.gameObject.SetActive(true);
        endButton.onClick.AddListener(() => RestartGameLoop());
    }

    private void RestartGameLoop()
    {
        StartCoroutine(currentMachine.SwitchState(parentObject.idleStartState));
    }

    private void ClearScreen()
    {
        playerRef.gameObject.SetActive(false);
        enemyManagerRef.ClearAllEnemies();
        enemyManagerRef.gameObject.SetActive(false);
        boardManagerRef.gameObject.SetActive(false);
    }
}
