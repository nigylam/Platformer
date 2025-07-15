using System;
using System.Collections;
using UnityEngine;

public class SmoothSliderBar : SliderBar
{
    [SerializeField] private float _changeSpeed = 0.1f;
    [SerializeField] private float _smoothStep = 0.01f;

    private float _previousValue = 0;
    private WaitForSeconds _smoothStepDelay;
    private Coroutine _smoothChange;

    private void Awake()
    {
        _smoothStepDelay = new WaitForSeconds(_changeSpeed);
    }

    public override void ChangeValue()
    {
        float currentValue = Convert.ToSingle(Health.Current) / Health.Max;

        if (_smoothChange != null)
            StopCoroutine(_smoothChange);

        _smoothChange = StartCoroutine(SmoothChangeValue(currentValue, _previousValue));
        _previousValue = currentValue;
    }

    private IEnumerator SmoothChangeValue(float currentValue, float previousValue)
    {
        while (previousValue != currentValue)
        {
            previousValue = Mathf.MoveTowards(previousValue, currentValue, _smoothStep);
            Slider.SetValueWithoutNotify(previousValue);

            yield return _smoothStepDelay;
        }
    }
}
