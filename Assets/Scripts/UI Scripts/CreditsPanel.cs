using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanel : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    

    public void SlideButtonsOut()
    {
        anim.SetTrigger("slideout");
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
