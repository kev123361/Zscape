using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private BoxCollider explosionCollider;
    public Vector2Int explosionPos;
    // Start is called before the first frame update
    void Start()
    {
        explosionCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        explosionCollider.size = Vector3.Lerp(explosionCollider.size, new Vector3(4f, 4f, 4f),Time.deltaTime);
    }
}
