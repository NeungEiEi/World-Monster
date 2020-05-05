using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBar : MonoBehaviour
{
    public Slider slider;
   
    public void SetMaxHealth(int monsterHealth)
    {
        slider.maxValue = monsterHealth;
    }

    public void SetHealth(int monsterHealth)
    {
        slider.value = monsterHealth;
    }
}
