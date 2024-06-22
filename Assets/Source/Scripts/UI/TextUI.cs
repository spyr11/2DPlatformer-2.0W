using TMPro;
using UnityEngine;

public class TextUI : BaseIndicatorUI
{
    [SerializeField] private TextMeshProUGUI _textCurrent;
    [SerializeField] private TextMeshProUGUI _textMax;

    protected override void ChangeValue(float value)
    {
        _textCurrent.text = value.ToString();
        _textMax.text = MaxValue.ToString();
    }
}
