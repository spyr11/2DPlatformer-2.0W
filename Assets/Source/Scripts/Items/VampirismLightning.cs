using UnityEngine;

public class VampirismLightning : MonoBehaviour
{
    [SerializeField] Transform _lightnig;
    [SerializeField] Transform _start;
    [SerializeField] Transform _end;

    private void Awake()
    {
        Disable();
    }

    public void SetDirection(Vector3 source, Vector3 target)
    {
        _lightnig.gameObject.SetActive(true);

        _start.position = source;
        _end.position = target;
    }

    public void Disable()
    {
        _lightnig.gameObject.SetActive(false);
    }
}