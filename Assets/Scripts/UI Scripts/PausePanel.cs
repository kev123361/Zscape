using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PausePanel : MonoBehaviour
{
    public bool paused;

    
    private Animator anim;

    public delegate void QuitEvent();
    public static event QuitEvent OnQuit;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        paused = false;
    }

    private void OnDisable()
    {
        paused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
            } else
            {
                Unpause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        anim.SetTrigger("slidein");
        paused = true;
    }

    public void Unpause()
    {
        
        anim.SetTrigger("slideout");
        StartCoroutine(DelayUnpause());
        paused = false;
    }
    
    private IEnumerator DelayUnpause()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        if (OnQuit != null)
        {
            OnQuit();
        }
    }
}
