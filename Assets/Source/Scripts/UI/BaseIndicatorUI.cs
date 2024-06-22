using UnityEngine;

public abstract class BaseIndicatorUI : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _indicator;

    private IIndicator _iIndicator;

    public float MaxValue => _iIndicator.MaxValue;

    private void OnValidate()
    {
        if (_indicator is not IIndicator)
        {
            Debug.Log(_indicator.name + " need implement " + nameof(IIndicator));

            _indicator = null;
        }
    }

    private void Awake()
    {
        _iIndicator = (IIndicator)_indicator;
    }

    private void OnEnable()
    {
        _iIndicator.Changed += OnValueChanged;
    }

    private void OnDisable()
    {
        _iIndicator.Changed -= OnValueChanged;
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    protected abstract void ChangeValue(float value);

    private void OnValueChanged(float value)
    {
        ChangeValue(value);
    }
}