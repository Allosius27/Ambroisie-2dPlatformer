using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public Slider slider { get; protected set; }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxBarValue(float duration)
    {
        slider.maxValue = duration;
        SetBarValue(duration);

    }

    public void SetBarValue(float duration)
    {
        slider.value = duration;

    }
}
