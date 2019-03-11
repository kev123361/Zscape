using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseEmitter : Pulse
{
    public bool emitOnMeasure;
    public bool emitOnBeat;
    public GameObject objectToEmit;
    public Vector3 whereToEmit;

    public override void OnBeat() {
        if (emitOnBeat) {
            GameObject.Instantiate(objectToEmit).transform.position = whereToEmit;
            Debug.Log("Emit on beat");
        }
    }

    public override void OnMeasure() {
        if (emitOnMeasure) {
            GameObject.Instantiate(objectToEmit).transform.position = whereToEmit;
            Debug.Log("Emit on measure");
        }
    }
}
