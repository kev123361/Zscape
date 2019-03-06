using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EndState : FlowState
{
    public override IEnumerator OnEnter()
    {
        Debug.Log("Enter EndState");
        endButton.gameObject.SetActive(true);
        ClearScreen();
        return base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override IEnumerator OnExit()
    {
        Debug.Log("Exit EndState");
        return base.OnExit();
    }

    private void ClearScreen()
    {
        playerRef.gameObject.SetActive(false);
        enemyManagerRef.gameObject.SetActive(false);
        boardManagerRef.gameObject.SetActive(false);
    }
}
