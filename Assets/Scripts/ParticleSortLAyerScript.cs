using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSortLAyerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().sortingLayerName = "Foreground";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
