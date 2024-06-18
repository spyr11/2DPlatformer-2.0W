using UnityEngine;

public class Coin : Item
{
    private float _amount = 100;

    public override float GetValue()
    {
        return _amount;
    }
}