using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Tentacle : MonoBehaviour
{
    LineRenderer lr;
    public Transform endPoint;
    public int subdivisions = 15;
    public float tentacleLengthScale = 1;
    Transform[] endpoints;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = subdivisions;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
    }

    void UpdateLine() {
        lr.enabled = true;

        Vector3[] bezierPoints = new Vector3[subdivisions];

        Vector3 a = transform.position;
        Vector3 ad = transform.forward * tentacleLengthScale;

        Vector3 b = endPoint.position;
        Vector3 bd = endPoint.forward * tentacleLengthScale;

        float delta = 1f / subdivisions;
        float progress = 0;
        for (int i = 0; i < subdivisions; i++) {

            bezierPoints[i] = Utility.TSpline(a, ad, b, bd, progress);
            progress += delta;
        }
        lr.SetPositions(bezierPoints);

        lr.widthMultiplier = transform.lossyScale.y;
    }

}
