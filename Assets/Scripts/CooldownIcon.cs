using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CooldownIcon : MonoBehaviour
{
    private float timer = 0f;
    private float currCooldown;
    private Image cdImage;

    // Start is called before the first frame update
    void Start()
    {
        cdImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        BoardManager.OnBeginRound += ResetCooldown;
    }

    private void OnDisable()
    {
        BoardManager.OnBeginRound -= ResetCooldown;
    }

    public void StartCooldown(float cooldown)
    {
        timer = 0f;
        currCooldown = cooldown;
        cdImage.fillAmount = 1f;
        StartCoroutine(Cooldown());
        
    }

    private IEnumerator Cooldown()
    {
        while (timer < currCooldown)
        {
            cdImage.fillAmount = Mathf.Lerp(1f, 0f, timer / currCooldown);
            yield return null;
            timer += Time.deltaTime;
        }
        cdImage.fillAmount = 0f;
    }

    public void ResetCooldown()
    {
        cdImage.fillAmount = 0f;
    }
}
