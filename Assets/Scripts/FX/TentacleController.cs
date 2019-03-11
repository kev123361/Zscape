using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleController : MonoBehaviour
{
    float progress;
    float rotationProgress;
    float scaleProgress;

    [Header("General")]
    public float changeSpeed = 1;
    public ParticleSystem emitter;
    public bool destroyOnTimeOver;
    public bool stopOnScaleOver;
    public float secondsTillDeath = 10;

    [Header("Rotation")]
    public bool startWithRandomRotation;
    public float rotationMultiplier = 5;
    public float rotationProgressSpeed = .1f;
    public AnimationCurve rotationCurve;

    [Header("Scaling")]
    public float scaleMultiplier = 1;
    public float scaleSpeed = .1f;
    public AnimationCurve scaleCurve;
    public AnimationCurve scaleCurveMultiplierCurve; // a single animation curve isn't precise enough

    [Header("Tentacles")]
    public int numTentacles = 3;
    public Transform tentaclesHolder;
    public float tentacleLengthScale;
    public AnimationCurve tentacleLengthScaleCurve;
    public Transform tentacleArm;


    [Header("Endpoints")]
    public Transform endPointsHolder;
    public float endpointDist;
    public Transform endpointArm;
    public AnimationCurve endpointDistScaleCurve;
    public float endpointsRotationMagnitude = 10;
    public AnimationCurve endpointsRotationCurve;

    // private
    Tentacle[] tentacles;
    Quaternion originalRotation;
    Vector3 originalScale;
    Transform[] endpointArms;
    Quaternion originalEndpointsRotation;
    
    // Start is called before the first frame update
    void Awake()
    {
        tentacles = new Tentacle[numTentacles];
        tentacles[0] = tentacleArm.GetComponentInChildren<Tentacle>();
        
        float angleStep = 360f / numTentacles;
        for (int i = 1; i < numTentacles; i++) {
            GameObject newTentacleArm = GameObject.Instantiate(tentacleArm.gameObject);
            GameObject newEndpointArm = GameObject.Instantiate(endpointArm.gameObject);

            newTentacleArm.transform.parent = tentaclesHolder;
            newEndpointArm.transform.parent = endPointsHolder;

            Quaternion rot = tentacleArm.rotation * Quaternion.Euler(new Vector3(angleStep * i, 0, 0));
            newTentacleArm.transform.rotation = rot;
            newEndpointArm.transform.rotation = rot;

            Tentacle t = newTentacleArm.GetComponentInChildren<Tentacle>();
            t.endPoint = newEndpointArm.transform.GetChild(0);
            tentacles[i] = t;
        }

        originalEndpointsRotation = endPointsHolder.localRotation;
        originalRotation = transform.localRotation;
        originalScale = transform.localScale;

        if (startWithRandomRotation) {
            transform.localRotation = originalRotation * Quaternion.Euler(Random.Range(0, 360), 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyOnTimeOver) {

            secondsTillDeath -= Time.deltaTime;
            if (secondsTillDeath < 0) {
                Destroy(gameObject);
                return;
            }
        }
        if (stopOnScaleOver && scaleProgress > 1) {
            return;
        }

        progress = progress > 1 ? 0 : progress;
        progress += Time.deltaTime * changeSpeed;

        rotationProgress = rotationProgress > 1 ? 0 : rotationProgress;
        rotationProgress += Time.deltaTime * rotationProgressSpeed;

        scaleProgress = scaleProgress > 1 ? 0 : scaleProgress;
        scaleProgress += Time.deltaTime * scaleSpeed;




        // change the length of the tentacles over time
        foreach (Tentacle t in tentacles) {
            t.tentacleLengthScale = tentacleLengthScale * tentacleLengthScaleCurve.Evaluate(progress) * transform.localScale.x;
        }

        // rotate the tips of the tentacles
        endPointsHolder.transform.localRotation = 
            originalEndpointsRotation 
            * Quaternion.Euler(endpointsRotationMagnitude * endpointsRotationCurve.Evaluate(progress), 0, 0);


        // rotate the whole object
        transform.localRotation = originalRotation * Quaternion.Euler(rotationMultiplier * rotationCurve.Evaluate(rotationProgress), 0, 0);

        // scale the whole object
        transform.localScale = new Vector3(1, 1, 1) * scaleMultiplier * scaleCurve.Evaluate(scaleProgress) * scaleCurveMultiplierCurve.Evaluate(scaleProgress);
        //emitter.gameObject.transform.localScale = transform.localScale;
        
    }
}
