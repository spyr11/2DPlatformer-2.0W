using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    private float _amount = 100;

    public void Overlap(IInteractable target)
    {
        if (target.GetType() == typeof(Backpack))
        {
            target.Use(_amount);

            Destroy(gameObject);
        }
    }
}