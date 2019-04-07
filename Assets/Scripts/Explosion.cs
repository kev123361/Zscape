using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private BoxCollider explosionCollider;
    public Vector2Int explosionPos;
    public BoardManager bm;
    // Start is called before the first frame update
    void Start()
    {
        explosionCollider = GetComponent<BoxCollider>();
        StartCoroutine(WarnInSquare());
    }

    // Update is called once per frame
    void Update()
    {
        explosionCollider.size = Vector3.LerpUnclamped(explosionCollider.size, new Vector3(4.75f, 4f, 4.75f),Time.deltaTime * 2f);
    }

    private void FixedUpdate()
    {
        
    }

    private IEnumerator WarnInSquare()
    {
        bm.GetTile(explosionPos.x, explosionPos.y).WarnTile();

        yield return new WaitForSeconds(.075f);

        bm.GetTile(explosionPos.x, explosionPos.y).UnwarnTile();

        for (int i = explosionPos.x - 1; i <= explosionPos.x + 1; i++ )
        {
            for (int j = explosionPos.y - 1; j <= explosionPos.y + 1; j++)
            {
                Tile currTile = bm.GetTile(i, j);
                if (currTile)
                {
                    currTile.WarnTile();
                }
            }
        }

        yield return new WaitForSeconds(.075f);

        for (int i = explosionPos.x - 1; i <= explosionPos.x + 1; i++)
        {
            for (int j = explosionPos.y - 1; j <= explosionPos.y + 1; j++)
            {
                Tile currTile = bm.GetTile(i, j);
                if (currTile)
                {
                    Debug.Log(currTile);
                    currTile.UnwarnTile();
                }
            }
        }

        
    }
}
