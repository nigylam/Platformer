using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothSliderTimeBar : MonoBehaviour
{
    [SerializeField] private AbilityTrigger _trigger;

    private Slider _slider;
    private Coroutine _smoothChange;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _trigger.StartedChanging += ChangeValue;
    }

    private void OnDisable()
    {
        _trigger.StartedChanging -= ChangeValue;
    }

    private void ChangeValue(float value)
    {
        if (_smoothChange != null)
            StopCoroutine(_smoothChange);

        _smoothChange = StartCoroutine(SmoothChangeValue(value));
    }

    private IEnumerator SmoothChangeValue(float duration)
    {
        _slider.value = Mathf.Round(_slider.value);

        float currentValue = _slider.value;
        float endValue = currentValue == 0 ? 1 : 0;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _slider.value = Mathf.Lerp(currentValue, endValue, elapsed / duration);
            yield return null;
        }

        _slider.value = endValue;
    }
}
