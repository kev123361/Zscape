using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpreads : MonoBehaviour
{
    [Header("IMPORTANT! The sizes of enemyPos and enemyRef must be equal!")]
    [Tooltip("IMPORTANT! The sizes of enemyPos and enemyRef must be equal!")]
    public List<Spread> spreads;

    public List<Spread> hardSpreads;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public Spread GetRandomSpread()
    {
        return spreads[Random.Range(0, spreads.Count)];
    }

    public Spread GetRandomHardSpread()
    {
        return hardSpreads[Random.Range(0, hardSpreads.Count)];
    }
    
}
