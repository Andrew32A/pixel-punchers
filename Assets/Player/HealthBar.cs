using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHeath(int health) {
        slider.maxValue = 100;
        slider.value = 100;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health) {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
