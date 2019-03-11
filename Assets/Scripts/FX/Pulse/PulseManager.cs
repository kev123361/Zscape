using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseManager : MonoBehaviour
{
    public float bpm = 90;
    public int beatsPerMeasure = 4;
    public int phraseLength = 4;
    float beatProgress;
    int currentBeat;
    float beatsPerSecond;

    Pulse[] pulseComponents;

    // Start is called before the first frame update
    void Start()
    {
        beatsPerSecond = 60f / bpm;
        pulseComponents = GetComponents<Pulse>();
    }

    // Update is called once per frame
    void Update()
    {
        if (beatProgress == 0 && currentBeat == 0) {

            Measure();
        }

        beatProgress += Time.deltaTime / beatsPerSecond;
        beatProgress = beatProgress > beatsPerMeasure ? 0 : beatProgress;

        if (beatProgress > 1) {
            currentBeat++;
            if (currentBeat == beatsPerMeasure) {
                // downbeat on new measure
                currentBeat = 0;
            } else {
                Beat();
            }
            beatProgress = 0;
        }

        //measureProgress += Time.deltaTime;
        
    }

    void Beat() {
        foreach (Pulse p in pulseComponents) {
            p.OnBeat();
            Debug.Log("Beat");
        }
    }

    void Measure() {

        foreach (Pulse p in pulseComponents) {
            p.OnMeasure();
            Debug.Log("Measure");
        }
    }
}
