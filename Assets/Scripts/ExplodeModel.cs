using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeModel : MonoBehaviour
{
    public float minForce;
    public float maxForce;
    public float radius;

    public void Explode()
    {
        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
                Destroy(t.gameObject, 2f);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Explode();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
