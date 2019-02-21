using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public BoardManager bm;

    private bool spawned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            Spawn();
            spawned = true;
        }
    }

    void Spawn()
    {
        bm.GetTile(0, 7).SpawnUnit(enemy, new Vector2Int(0, 7), bm);
    }
}
