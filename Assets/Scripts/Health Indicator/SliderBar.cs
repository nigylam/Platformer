using System;
using UnityEngine.UI;
using UnityEngine;

public class SliderBar : Bar
{
    [SerializeField] protected Slider Slider;

    public override void ChangeValue()
    {
        float currentValue = Convert.ToSingle(Health.Current) / Health.Max;

        Slider.SetValueWithoutNotify(currentValue);
    }
}
