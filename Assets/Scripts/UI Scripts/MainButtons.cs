using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for UI Button animations
[RequireComponent(typeof(Animator))]
public class MainButtons : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlideInButtons()
    {
        anim.SetTrigger("slidein");
    }

    public void SlideOutButtons()
    {
        anim.SetTrigger("slideout");
    }
}
