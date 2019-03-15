using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spread
{
    [SerializeField]
    private List<Vector2Int> enemyPos;
    [SerializeField]
    private List<GameObject> enemyRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Vector2Int> GetEnemyPositions()
    {
        return enemyPos;
    }

    public List<GameObject> GetEnemyRefs()
    {
        return enemyRef;
    }
    
}
