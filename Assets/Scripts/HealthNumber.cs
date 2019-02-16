using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNumber : MonoBehaviour
{
    private Unit unit;
    private TextMesh text;
    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponentInParent<Unit>();
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = unit.health.ToString();
    }
}
