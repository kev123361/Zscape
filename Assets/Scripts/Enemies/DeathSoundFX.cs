using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSoundFX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DieSoon());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DieSoon()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
