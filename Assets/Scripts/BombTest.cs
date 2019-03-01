using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTest : MonoBehaviour
{
    public BoardManager bm;
    public GameObject bomb;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        BoardManager.OnBeginRound += StartBombs;

    }

    private void OnDisable()
    {
        BoardManager.OnBeginRound -= StartBombs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartBombs()
    {
        StartCoroutine(SpawnBomb());
    }

    private IEnumerator SpawnBomb()
    {
        Tile currTile = bm.GetTile(Mathf.FloorToInt(Random.Range(0, 3)), Mathf.FloorToInt(Random.Range(0, 4)));
        Debug.Log(currTile);
        currTile.SpawnUnit(bomb, bm);

        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnBomb());
    }
}
