using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text displayText;

    public int max;
    public int current;

    void Start()
    {
        displayText.text = current + "/" + max;
        slider.minValue = 0;
        slider.maxValue = max;
        slider.value = current;
    }

    void Update()
    {
        displayText.text = current + "/" + max;
        slider.maxValue = max;
        slider.value = current;
    }
}
