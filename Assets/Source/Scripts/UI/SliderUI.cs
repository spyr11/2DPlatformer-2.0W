using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : BaseIndicatorUI
{
    [SerializeField] private Slider _slider;
    [SerializeField] private bool _isSmooth;

    private Coroutine _coroutine;

    protected override void ChangeValue(float value)
    {
        float newValue = value / MaxValue;

        if (_isSmooth)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(SlideSmooth(newValue));
        }
        else
        {
            _slider.value = newValue;
        }
    }

    private IEnumerator SlideSmooth(float value)
    {
        while (_slider.value != value)
        {
            float current = _slider.value;

            float delta = Mathf.Abs(current - value);

            _slider.value = Mathf.MoveTowards(current, value, delta * Time.deltaTime);

            yield return null;
        }
    }
}
