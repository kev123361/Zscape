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

    public Button upgradeButton1
    {
        get
        {
            return parentObject.upgradeUI1;
        }
    }

    public Button howtoplayButton
    {
        get
        {
            return parentObject.howtoplayUI;
        }
    }

    public Button upgradeButton2
    {
        get
        {
            return parentObject.upgradeUI2;
        }
    }

    public Button upgradeButton3
    {
        get
        {
            return parentObject.upgradeUI3;
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

    public Button optionsButton
    {
        get
        {
            return parentObject.optionsUI;
        }
    }

    public Button creditsButton
    {
        get
        {
            return parentObject.creditsUI;
        }
    }

    public RectTransform upgradePanel
    {
        get
        {
            return parentObject.UpgradePanel;
        }
    }

    public Text titleCard
    {
        get
        {
            return parentObject.titleCard;
        }
    }

    public RectTransform gameOverPanel
    {
        get
        {
            return parentObject.GameOverPanel;
        }
    }
}
