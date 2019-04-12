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
        ShowButtons();
        boardManagerRef.setPersistentHealth(playerRef.getMaxHealth());
        return base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override IEnumerator OnExit()
    {
        Debug.Log("Exit StartState");
        ResetStats();
        startButton.onClick.RemoveAllListeners();
        return base.OnExit();
    }

    private void StartGameplayLoop()
    {
        Debug.Log("button linked");
        //Logic to begin gameplay loop by calling the board manager

        //Deactivate ALL UI
        HideButtons();
        //Chagnes the state to the 'PlayState' in the state machine
        StartCoroutine(currentMachine.SwitchState(parentObject.playState));
    }

    private void ShowButtons()
    {
        titleCard.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        howtoplayButton.gameObject.SetActive(true);
    }
    
    private void HideButtons()
    {
        titleCard.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        howtoplayButton.gameObject.SetActive(false);
    }

    private void ResetStats()
    {
        enemyManagerRef.ResetCounter();
        boardManagerRef.ResetStats();
    }

}
