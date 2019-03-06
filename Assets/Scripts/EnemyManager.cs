using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject gearEnemy;
    public GameObject shooterEnemy;
    public BoardManager bm;

    [SerializeField]
    private List<Enemy> enemyList = new List<Enemy>();

    private int numEnemies = 9999;

    private bool spawned;

    public delegate void RoundWin();
    public static event RoundWin OnRoundWin;

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
        numEnemies = 3;
        bm.GetTile(0, 7).SpawnUnit(gearEnemy, bm);
        bm.GetTile(1, 6).SpawnUnit(shooterEnemy, bm);
        bm.GetTile(2, 7).SpawnUnit(gearEnemy, bm);

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
