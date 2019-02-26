using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowState : State<GameFlowController>
{
    //These are references to the classes you will be affecting through your states. Simple inheritance and creating get/setters for them.
    public BoardManager boardManagerRef
    {
        get
        {
            return parentObject.bmRef;
        }
    }

    public EnemyManager enemyManagerRef
    {
        get
        {
            return parentObject.emRef;
        }
    }

    public Button startButton
    {
        get
        {
            return parentObject.startUI;
        }
    }

    public Button upgradeButton
    {
        get
        {
            return parentObject.upgradeUI;
        }
    }

    public Button endButton
    {
        get
        {
            return parentObject.endUI;
        }
    }

    public Player playerRef
    {
        get
        {
            return parentObject.player;
        }
    }
}
