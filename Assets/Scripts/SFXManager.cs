using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public AudioClip ButtonFX;
    public AudioClip UpgradeFX;

    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonFX()
    {
        audio.PlayOneShot(ButtonFX);
    }

    public void PlayUpgradeFX()
    {
        audio.PlayOneShot(UpgradeFX);
    }

    public void PlayHitFX()
    {
        audio.PlayOneShot(ButtonFX);
    }
}
