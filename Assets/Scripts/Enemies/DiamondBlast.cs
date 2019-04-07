using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiamondBlast : MonoBehaviour {


    public GameObject outerCube;
    public GameObject middleCube;
    public GameObject innerCube;
    public GameObject projectile;
    private Transform outerTransform;
    private Transform middleTransform;
    private Transform innerTransform;
    private float rotateSpeed = .5f;
    private float coolTime = 18.9f;
    private float curTime;
    private float coolDown = 2f;
    private bool firing = false;
    private Transform startPos;

    // Use this for initialization
    void Start() {
        outerTransform = outerCube.transform;
        middleTransform = middleCube.transform;
        innerTransform = innerCube.transform;
        curTime = coolDown;
        startPos = projectile.transform;
    }

    // Update is called once per frame
    void Update() {
        curTime += Time.deltaTime;
        if (curTime >= coolDown && curTime <= coolTime)
        {
            outerTransform.Rotate(Vector3.up, rotateSpeed);
            middleTransform.Rotate(Vector3.up, -rotateSpeed);
            innerTransform.Rotate(Vector3.up, rotateSpeed);
        }
        else if (curTime > coolTime)
        {
            curTime = 0.0f;

            firing = false;
            Fire();
        }

    }


    void Fire ()
    {
        projectile.SetActive(true);
        // Put the fire method here

    }
}
