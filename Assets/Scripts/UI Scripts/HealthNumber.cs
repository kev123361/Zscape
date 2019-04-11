using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthNumber : MonoBehaviour
{
    public Text healthText;
    public Text damageText;
    public Text fadingDamageText;
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
    private float currentDamage = 0f;
    private const float baseAppearTime = 0.75f;
    private const float baseFadeTime = 0.5f;
    private static Color RED = new Color(0.849f, 0.11886f, 0.11886f);
    private static Color YELLOW = new Color(0.9433416f, 0.95f, 0.13965f);

    void Awake()
    {
        // Only player health bar will have health text, so it can be null on enemies
        if (damageText == null)
        {
            Debug.LogError("No health_text found.");
        }
        if (fadingDamageText == null)
        {
            Debug.LogError("No fading_damage_text found.");
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
                // set fading damage and reset current damage
                fadingDamageText.text = currentDamage.ToString();
                fadingDamageText.enabled = true;
                fadingDamageText.color = damageText.color;
                damageText.enabled = false;
                damageText.color = RED;
                currentDamage = 0f;
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
            fadingDamageText.color = new Color(fadingDamageText.color.r, fadingDamageText.color.g, fadingDamageText.color.b, (fadingHealthTimer / baseFadeTime));
            // if fade timer runs out
            if (fadingHealthTimer <= 0)
            {
                fadingHealthStart = 0f;
                fadingHealthEnd = 0f;
                fadingDamageText.enabled = false;
            }
            fadingWhiteHealthRect.anchorMin = new Vector2(fadingHealthStart, 0);
            fadingWhiteHealthRect.anchorMax = new Vector2(fadingHealthEnd,  1);
        }
    }
    
    public void UpdateHealth(float health, float maxHealth, float damage)
    {
        UpdateHealth(health, maxHealth, damage, false);
    }

    public void UpdateHealth(float health, float maxHealth, float damage, bool critical)
    {
        // Set health text on player
        if (healthText != null) {
            healthText.text = ((int) health).ToString();
        }
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
            // Add taken damage to current damage
            currentDamage += damage;
            damageText.enabled = true;
            if (critical) {
                damageText.color = YELLOW;
            }
            StartCoroutine(DamageNumberShake());
        }
        // Set damage text to whole number
        damageText.text = ((int) currentDamage).ToString();
    }

    private IEnumerator DamageNumberShake()
    {
        // In radians
        float randomDirection = Random.Range(0, 2 * Mathf.PI);
        float sinusoidalMagnitude;
        float decayingMagnitude = 25f;
        for (int i = 0; i < 30; i++) {
            sinusoidalMagnitude = Mathf.Sin(15 * i / (2 * Mathf.PI));
            // decay magnitude
            decayingMagnitude = Mathf.Lerp(decayingMagnitude, 0, 0.15f);
            damageText.rectTransform.localPosition = new Vector3((decayingMagnitude * sinusoidalMagnitude * Mathf.Cos(randomDirection)),
                                                                 (decayingMagnitude * sinusoidalMagnitude * Mathf.Sin(randomDirection)),
                                                                 0);
            yield return new WaitForSeconds(1/60f);
        }
    }

}
