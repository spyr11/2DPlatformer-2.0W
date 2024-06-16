using UnityEngine;

public class Medicine : MonoBehaviour, IItem
{
    private float _healValue = 50f;

    public void Overlap(IInteractable target)
    {
        if (target.GetType() == typeof(HealthComponent))
        {
            target.Use(_healValue);

            Destroy(gameObject);
        }
    }
}