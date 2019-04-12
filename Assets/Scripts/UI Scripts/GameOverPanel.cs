using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{

    private Animator anim;

    public Text timeText;
    public Text roundText;
    public Text enemyText;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlideButtonsOut()
    {
        anim.SetTrigger("slideout");
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetTimeText(string str)
    {
        timeText.text = str;
    }

    public void SetRoundText(string str)
    {
        roundText.text = str;
    } 
    
    public void SetEnemiesDefeatedText(string str)
    {
        enemyText.text = str;
    }
}
