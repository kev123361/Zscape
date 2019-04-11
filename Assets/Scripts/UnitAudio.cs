using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UnitAudio : MonoBehaviour
{
    private AudioSource audioSource;

    public GameObject DeathFXObject;
    public AudioClip shootSFX;
    public AudioClip deathSFX;
    public AudioClip bombThrowSFX;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayShootSFX()
    {
        audioSource.PlayOneShot(shootSFX);
    }

    public void PlayDeathSFX()
    {
        Instantiate(DeathFXObject);
        audioSource.PlayOneShot(deathSFX);
    }

    public void PlayThrowSFX()
    {
        audioSource.PlayOneShot(bombThrowSFX);
    }
}
