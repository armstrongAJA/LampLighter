using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public void Start()
    {
        slider = gameObject.GetComponentInChildren<Slider>();
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;//set max value to health value
        slider.value = health;//set slider value to max health to initialise
    }
}
