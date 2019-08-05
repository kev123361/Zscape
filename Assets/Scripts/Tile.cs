﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material baseMat;
    public Material dangerMat;
    public Vector2Int pos;

    private Renderer tileRenderer;
    private Space space;

    // Start is called before the first frame update
    void Start()
    {
        tileRenderer = GetComponent<Renderer>();
        space = GetComponentInChildren<Space>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WarnTile()
    {
        space.isAttack = true;
    }

    public void UnwarnTile()
    {
        space.isAttack = false;
    }

    public void WarnBombTile()
    {
        space.isBombAttack = true;
    }

    public void UnwarnBombTile()
    {
        space.isBombAttack = false;
    }

    public GameObject SpawnUnit(GameObject unit, BoardManager bm)
    {
        Debug.Log("This was an enemy unit: " +
            unit.GetComponent<Enemy>().ToString());
        

        //Conditional will make this method more modular so we can spawn in other units such as players, field items, etc.
        if (unit.GetComponent<Enemy>())
        {
            Debug.Log("Enemy Spawn Logic");
            
            GameObject newUnit = Instantiate(unit, transform.position + new Vector3(0f, (unit.GetComponent<BoxCollider>().size.y * unit.transform.localScale.y) / 2, 0f), 
                transform.rotation * Quaternion.Euler(0,180,0));
            //newUnit.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
            Enemy enemy = newUnit.GetComponent<Enemy>();
            Debug.Log("enemy health: " + enemy.health);
            enemy.pos = pos;
            enemy.bm = bm;
            
            return newUnit;

        } else if (unit.GetComponent<Bomb>())
        {
            GameObject newUnit = Instantiate(unit, transform.position + new Vector3(0f, 5f, 0f),
                Quaternion.identity);
            newUnit.GetComponent<Bomb>().explosionPos = pos;
            newUnit.GetComponent<Bomb>().bm = bm;
            return newUnit;
        } else
        {
           GameObject newUnit = Instantiate(unit, transform.position + new Vector3(0f, (unit.GetComponent<BoxCollider>().size.y * unit.transform.localScale.y) / 2, 0f), Quaternion.identity);
            return newUnit;
        }
        
        
    }
}

