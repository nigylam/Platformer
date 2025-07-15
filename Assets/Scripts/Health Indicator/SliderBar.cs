using System;
using UnityEngine.UI;
using UnityEngine;

public class SliderBar : Bar
{
    protected Slider Slider;

    private void Awake()
    {
        Initialize();
    }

    public override void ChangeValue()
    {
        float currentValue = Convert.ToSingle(Health.Current) / Health.Max;

        Slider.SetValueWithoutNotify(currentValue);
    }

    protected virtual void Initialize()
    {
        Slider = GetComponent<Slider>();
    }
}
