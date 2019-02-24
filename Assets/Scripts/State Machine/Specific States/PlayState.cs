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
}
