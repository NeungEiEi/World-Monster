using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int playerHealth)
    {
        slider.maxValue = playerHealth;
    }

    public void SetHealth(int playerHealth)
    {
        slider.value = playerHealth;
    }
}
