using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingProjectile : Projectile
{
    public MovingProjectile(float speed, float acceleration) : base (speed, acceleration)
    {
        this.speed = speed;
        this.acceleration = acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
