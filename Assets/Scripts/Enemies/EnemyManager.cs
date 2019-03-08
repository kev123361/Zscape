using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject gearEnemy;
    public GameObject shooterEnemy;
    public BoardManager bm;
    private EnemySpreads enemySpreads;

    [SerializeField]
    private List<Enemy> enemyList = new List<Enemy>();

    private int numEnemies = 9999;

    private bool spawned;

    public delegate void RoundWin();
    public static event RoundWin OnRoundWin;

    private void Start()
    {
        enemySpreads = GetComponent<EnemySpreads>();
    }
    // Subscribing Spawn() to the OnBeginRound event
    private void OnEnable()
    {
        BoardManager.OnBeginRound += Spawn;
        Enemy.OnEnemyDeath += EnemyDied;
    }

    private void OnDisable()
    {
        BoardManager.OnBeginRound -= Spawn;
        Enemy.OnEnemyDeath -= EnemyDied;
    }

    public void SetNumEnemies(int num)
    {
        numEnemies = num;
    }

    void Spawn()
    {
        
        Spread newSpread = enemySpreads.GetRandomSpread();
        List<Vector2Int> newEnemyPos = newSpread.GetEnemyPositions();
        List<GameObject> newEnemies = newSpread.GetEnemyRefs();
        numEnemies = newEnemies.Count;

        for (int i = 0; i < newEnemies.Count; i++)
        {
            bm.GetTile(newEnemyPos[i].x, newEnemyPos[i].y).SpawnUnit(newEnemies[i], bm);
        }

    }

    // Enemies will need to make this call up to the manager before they die
    public void EnemyDied()
    {
        numEnemies -= 1;
        if (numEnemies <= 0)
        {
            //GameObject.FindGameObjectWithTag("State Machine").GetComponent<GameFlowController>().RoundComplete();
            if (OnRoundWin != null)
                OnRoundWin();
        }
    }
}
