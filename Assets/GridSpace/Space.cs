using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    public bool isAttack;
    public bool isPlayer;
    public float lerpSpeed = 0.1f;
    [Header("Idle Shader Values")]
    public float idleWaveSpeed = 0.063f;
    public float idleWaveAmount = 0.087f;
    public float idleWaveHeight = 0.078f;
    public Color idleColor = new Color(0.990566f, 0.990566f, 0.990566f);
    public Color idleFresnelColor = new Color(0.9150943f, 0.5842218f, 0.125178f);
    [Header("Player Shader Values")]
    public float playerWaveSpeed = 0.097f;
    public float playerWaveAmount = 0.087f;
    public float playerWaveHeight = 0.24f;
    public Color playerColor = new Color(0.51998043f, 0.8164914f, 0.8962264f);
    public Color playerFresnelColor = new Color(0.2988163f, 0.7402372f, 0.745283f);
    [Header("Attack Shader Values")]
    public float attackWaveSpeed = 0.4f;
    public float attackWaveAmount = 1;
    public float attackWaveHeight = 1;
    public Color attackColor = new Color(1, 0.1254468f, 0.07843135f);
    public Color attackFresnelColor = new Color(0.8588235f, 0.4467003f, 0.2313725f);
    
    // current shader values
    float waveSpeed;
    float waveAmount;
    float waveHeight;
    Color color;
    Color fresnelColor;
    public Renderer r;
    Material m;
    // Start is called before the first frame update
    void Start()
    {
        waveSpeed = idleWaveSpeed;
        waveAmount = idleWaveAmount;
        waveHeight = idleWaveHeight;
        color = idleColor;
        fresnelColor = idleFresnelColor;
        m = r.material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAttack) {
            waveSpeed = Mathf.Lerp(waveSpeed, attackWaveSpeed, lerpSpeed);
            waveAmount = Mathf.Lerp(waveAmount, attackWaveAmount, lerpSpeed);
            waveHeight = Mathf.Lerp(waveHeight, attackWaveHeight, lerpSpeed);
            color = Color.Lerp(color, attackColor, lerpSpeed);
            fresnelColor = Color.Lerp(fresnelColor, attackFresnelColor, lerpSpeed);
        } else if (isPlayer) {
            waveSpeed = Mathf.Lerp(waveSpeed, playerWaveSpeed, lerpSpeed);
            waveAmount = Mathf.Lerp(waveAmount, playerWaveAmount, lerpSpeed);
            waveHeight = Mathf.Lerp(waveHeight, playerWaveHeight, lerpSpeed);
            color = Color.Lerp(color, playerColor, lerpSpeed);
            fresnelColor = Color.Lerp(fresnelColor, playerFresnelColor, lerpSpeed);
        } else {
            waveSpeed = Mathf.Lerp(waveSpeed, idleWaveSpeed, lerpSpeed);
            waveAmount = Mathf.Lerp(waveAmount, idleWaveAmount, lerpSpeed);
            waveHeight = Mathf.Lerp(waveHeight, idleWaveHeight, lerpSpeed);
            color = Color.Lerp(color, idleColor, lerpSpeed);
            fresnelColor = Color.Lerp(fresnelColor, idleFresnelColor, lerpSpeed);
        }
        m.SetFloat("_Speed", waveSpeed);
        m.SetFloat("_Amount", waveAmount);
        m.SetFloat("_Height", waveHeight);
        m.SetColor("_Color", color);
        m.SetColor("_FresnelColor", fresnelColor);
    }

    private void OnDestroy() {
        Destroy(m);
    }
}
