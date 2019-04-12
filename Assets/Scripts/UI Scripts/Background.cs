using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Shader regularFlow;
    public Shader redFlow;

    public BoardManager bm;

    public Renderer background;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        background = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBackground()
    {
        if (bm.GetLevel() < 4)
        {
            SetRegularFlow();
        } else
        {
            SetRedFlow();
        }
    }

    public void SetRegularFlow()
    {
        background.material.shader = regularFlow;
        background.material.SetColor("_TintColor", new Color(1, 1, 1));
    }

    public void SetRedFlow()
    {
        background.material.shader = redFlow;
        background.material.SetColor("_TintColor", new Color(.79f, .37f, .37f));
    }
}
