using System;
using UnityEngine;

public class Picker : MonoBehaviour
{
    private bool _isPicked;

    public event Action<float> HealPicked;
    public event Action<float> CoinPicked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item) && item.IsPicked == false)
        {
            _isPicked = true;

            if (item is Medicine)
            {
                HealPicked?.Invoke(item.GetValue());
            }

            else if (item is Coin)
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