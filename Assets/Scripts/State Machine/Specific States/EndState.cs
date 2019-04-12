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
        SetStats();
        ClearScreen();

        //Telemetrics
        ResetPlayerStats();

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
        gameOverPanel.gameObject.SetActive(true);
        endButton.gameObject.SetActive(true);
        endButton.onClick.AddListener(() => RestartGameLoop());
        //ENTER FUNCTIONALITY TO DISPLAY LOGISTICS
        //boardManagerRef.GetLevel();
    }

    private void SetStats()
    {
        GameOverPanel endScreen = gameOverPanel.gameObject.GetComponent<GameOverPanel>();
        endScreen.SetRoundText("Rounds Completed: " + (boardManagerRef.GetLevel() - 1).ToString());
        endScreen.SetTimeText("Time: " + boardManagerRef.GetEndTime());
        endScreen.SetEnemiesDefeatedText("Enemies Defeated: " + enemyManagerRef.GetEnemiesDefeated().ToString());
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

    private void ResetPlayerStats()
    {
        playerRef.ResetStats();
    }
}
