using UnityEngine;

public class Backpack : MonoBehaviour, IInteractable
{
    private int _coinsCount = 0;

    public void Use(float value)
    {
        _coinsCount += (int)value;
    }
}