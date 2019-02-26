using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTest : MonoBehaviour
{
    public BoardManager bm;
    public GameObject bomb;

    private bool started;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            started = true;
            StartCoroutine(SpawnBomb());
        }
    }

    private IEnumerator SpawnBomb()
    {
        Tile currTile = bm.GetTile(Mathf.FloorToInt(Random.Range(0, 3)), Mathf.FloorToInt(Random.Range(0, 8)));
        Debug.Log(currTile);
        currTile.SpawnUnit(bomb, bm);

        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnBomb());
    }
}
