using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    private Image health_UI;
    void Awake()
    {
        health_UI = GameObject.FindWithTag(Tags.HEALTH_UI).GetComponent<Image>();
    }

    public void DisplayHealth(float Value)
    {
        Value /= 100f;
        if (Value < 0f)
            Value = 0f;
        health_UI.fillAmount = Value;
    }
}//class
