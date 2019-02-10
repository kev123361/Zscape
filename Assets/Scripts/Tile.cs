using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material baseMat;
    public Material dangerMat;

    private Renderer tileRenderer;

    // Start is called before the first frame update
    void Start()
    {
        tileRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WarnTile()
    {
        tileRenderer.material = dangerMat;
    }

    public void UnwarnTile()
    {
        tileRenderer.material = baseMat;
    }

    public void SpawnUnit(GameObject unit, BoardManager bm)
    {
        GameObject newUnit = Instantiate(unit, transform.position + new Vector3(0f, unit.GetComponent<BoxCollider>().size.y / 2, 0f), Quaternion.identity);

        //Conditional will make this method more modular so we can spawn in other units such as players, field items, etc.
        if(newUnit.GetComponent<Enemy>())
        {
            Enemy enemy = newUnit.GetComponent<Enemy>();
            enemy.bm = bm;
            enemy.timeToShoot = 5;
            enemy.bulletAccel = 10;
        }
        
        
    }
}

