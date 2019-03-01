using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject gearEnemy;
    public GameObject shooterEnemy;
    public BoardManager bm;

    private int numEnemies = 9999;

    private bool spawned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Subscribing Spawn() to the OnBeginRound event
    private void OnEnable()
    {
        BoardManager.OnBeginRound += Spawn;
    }

    private void OnDisable()
    {
        BoardManager.OnBeginRound -= Spawn;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            GameObject.FindGameObjectWithTag("State Machine").GetComponent<GameFlowController>().GameOver();

        }
    }
}
