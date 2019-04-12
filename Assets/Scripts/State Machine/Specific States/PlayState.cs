using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayState : FlowState
{
    public override IEnumerator OnEnter()
    {
        Debug.Log("Enter PlayState");
        ImageGameObj();
        boardManagerRef.StartBattle();
        playerRef.setHealth(boardManagerRef.getPersistentHealth());
        pausePanel.gameObject.SetActive(true);
        //Create an event listener for death status
        Player.OnDeath += PlayerDeath;
        //Create an event listener for round win status
        EnemyManager.OnRoundWin += RoundComplete;
        //Create event listener for quit to menu
        PausePanel.OnQuit += QuitToMenu;
        return base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override IEnumerator OnExit()
    {
        Debug.Log("Exit PlayState");
        Player.OnDeath -= PlayerDeath;
        pausePanel.gameObject.SetActive(false);
        EnemyManager.OnRoundWin -= RoundComplete;
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

    private void QuitToMenu() {
        StartCoroutine(currentMachine.SwitchState(parentObject.endState));
    }

    private void ImageGameObj()
    {
        playerRef.gameObject.SetActive(true);
        enemyManagerRef.gameObject.SetActive(true);
        boardManagerRef.gameObject.SetActive(true);
    }
}
