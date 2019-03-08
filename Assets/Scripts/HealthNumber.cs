using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthNumber : MonoBehaviour
{
    public Text text;
    public Slider slider;

    void Awake()
    {
        if (text == null) {
            Debug.LogError("No health_text found.");
        }
        if (slider == null) {
            Debug.LogError("No health_slider found.");
        }
    }

    void Update()
    {
        // empty
    }

    public void UpdateHealth(float health, float maxHealth)
    {
        print("Called update");
        text.text = ((int) health).ToString();
        slider.value = health / maxHealth;
    }

}
