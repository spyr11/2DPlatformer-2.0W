using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public bool IsPicked { get; private set; }

    private void OnEnable()
    {
        IsPicked = false;
    }

    private void OnDisable()
    {
        IsPicked = true;
    }

    public abstract float GetValue();
}