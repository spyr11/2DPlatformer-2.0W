using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] private HealthComponent _health;
    [SerializeField] private Backpack _backpack;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<IItem>(out IItem item))
        {
            item.Overlap(_health);
            item.Overlap(_backpack);
        }
    }
}