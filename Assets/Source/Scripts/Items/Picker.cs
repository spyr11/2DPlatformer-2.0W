using System;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(Backpack))]
public class Picker : MonoBehaviour
{
    private IChangeable _healthComponent;
    private IChangeable _backpack;

    private bool _isPicked;

    private event Action<float> HealPicked;
    private event Action<float> CoinPicked;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _backpack = GetComponent<Backpack>();
    }

    private void OnEnable()
    {
        HealPicked += _healthComponent.OnPicked;
        CoinPicked += _backpack.OnPicked;
    }

    private void OnDisable()
    {
        HealPicked -= _healthComponent.OnPicked;
        CoinPicked -= _backpack.OnPicked;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Item>(out Item item) && _isPicked == false)
        {
            _isPicked = true;

            if (item is Medicine)
            {
                HealPicked?.Invoke(item.GetValue());
            }

            if (item is Coin)
            {
                CoinPicked?.Invoke(item.GetValue());
            }

            item.gameObject.SetActive(false);
            Destroy(item.gameObject);
        }

        if (item == null)
        {
            _isPicked = false;
        }
    }
}