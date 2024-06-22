using System;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public event Action<float> HealPicked;
    public event Action<float> CoinPicked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item) && item.IsPicked == false)
        {
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
    }
}