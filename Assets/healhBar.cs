using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class healhBar : MonoBehaviour
{

    public Slider slider;
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
