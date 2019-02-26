using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public StateMachine<GameFlowController> stateMachine;

    public string currentStateName;

    #region State List
    public IdleStartState idleStartState;
    public PlayState playState;
    public UpgradeState upgradeState;
    public EndState endState;

    #endregion

    //Contains everything that is subject to change
    #region Variable List
    public BoardManager bmRef;
    public EnemyManager emRef;
    public Player player;
    #endregion


    #region World Triggers
    public Button startUI;
    public Button upgradeUI;
    public Button endUI;
    public Canvas UICanvas;

    #endregion

    #region Conditional List
    bool isPlaying = false;
    bool isUpgrading = false;

    #endregion

    void Awake()
    {
        stateMachine = new StateMachine<GameFlowController>(this);
        Debug.Log("State machine created");
        //Start in a opening state
        StartCoroutine(stateMachine.SwitchState(idleStartState));
    }
    void Update()
    {
        stateMachine.Update();
        currentStateName = stateMachine.currentState.ToString();
    }
}
