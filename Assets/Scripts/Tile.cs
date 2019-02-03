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
}

