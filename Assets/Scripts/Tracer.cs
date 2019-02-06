using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracer : Projectile
{
    public Tracer (float speed, float acceleration) : base(speed, acceleration)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
