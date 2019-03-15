using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthNumber : MonoBehaviour
{
    public Text text;
    public Slider slider;
    public RectTransform whiteHealthRect;
    public RectTransform fadingWhiteHealthRect;

    private Image sliderFill;
    private Image fadingHealthFill;
    // Marks where the white health should start and end. From 0 to 1.
    private float whiteHealthStart = 0f;
    private float whiteHealthEnd = 0f;
    private float fadingHealthStart = 0f;
    private float fadingHealthEnd = 0f;
    private float whiteHealthTimer = 0f;
    private float fadingHealthTimer = 0f;
    private const float baseAppearTime = 0.75f;
    private const float baseFadeTime = 0.5f;

    void Awake()
    {
        if (text == null)
        {
            Debug.LogError("No health_text found.");
        }
        if (slider == null)
        {
            Debug.LogError("No health_slider found.");
        }
        if (whiteHealthRect == null)
        {
            Debug.LogError("No white_health_rect found.");
        }
        sliderFill = slider.fillRect.GetComponent<Image>();
        fadingHealthFill = fadingWhiteHealthRect.GetComponent<Image>();
    }

    void Update()
    {
        // Show white health briefly
        if (whiteHealthTimer > 0)
        {
            // Decrement white health timer
            whiteHealthTimer -= Time.deltaTime;
            // If timer runs out, start fade and hide white health
            if (whiteHealthTimer <= 0)
            {
                fadingHealthStart = whiteHealthStart;
                fadingHealthEnd = whiteHealthEnd;
                fadingHealthTimer = baseFadeTime;
                whiteHealthStart = 0f;
                whiteHealthEnd = 0f;
            }
            whiteHealthRect.anchorMin = new Vector2(whiteHealthStart, 0);
            whiteHealthRect.anchorMax = new Vector2(whiteHealthEnd, 1);

        }
        if (fadingHealthTimer > 0)
        {
            // Decrement fade timer
            fadingHealthTimer -= Time.deltaTime;
            // Move fading health upward
            fadingWhiteHealthRect.anchoredPosition = new Vector2(0.5f, 100 * Mathf.Pow((1f - (fadingHealthTimer / baseFadeTime)), 2f));
            // Fade the health over time
            fadingHealthFill.color = new Color(fadingHealthFill.color.r, fadingHealthFill.color.g, fadingHealthFill.color.b, (fadingHealthTimer / baseFadeTime));
            // if fade timer runs out
            if (fadingHealthTimer <= 0)
            {
                fadingHealthStart = 0f;
                fadingHealthEnd = 0f;
                //fadingWhiteHealthRect.localPosition = Vector3.zero;
            }
            fadingWhiteHealthRect.anchorMin = new Vector2(fadingHealthStart, 0);
            fadingWhiteHealthRect.anchorMax = new Vector2(fadingHealthEnd,  1);
        }
    }

    public void UpdateHealth(float health, float maxHealth, float damage)
    {
        // Set text to whole number
        text.text = ((int) health).ToString();
        float proportion = health / maxHealth;
        // Set value based on proportion of health
        slider.value = proportion;
        // Set color of bar. Hue is 135 (green) at 100% and 0 (red) at 25% and below
        float hueValue = Mathf.Max((proportion * 180) - 45, 0);
        Color sliderColor = Color.HSVToRGB(hueValue / 360f, 1f, 1f);
        sliderFill.color = sliderColor;
        
        // Display damage taken as white health
        if (damage > 0)
        {
            if (whiteHealthEnd <= 0f)
            {
                whiteHealthEnd = (health + damage) / maxHealth;
            }
            whiteHealthStart = health / maxHealth;
            whiteHealthTimer = baseAppearTime;
        }
    }

}
