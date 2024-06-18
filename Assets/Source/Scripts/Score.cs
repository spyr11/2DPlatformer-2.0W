using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private Backpack _backpack;
    [SerializeField] private TextMeshProUGUI _textMesh;

    private void OnEnable()
    {
        _backpack.ValueChanged += AddScore;
    }

    private void OnDisable()
    {
        _backpack.ValueChanged -= AddScore;
    }

    private void AddScore(float value)
    {
        _textMesh.text = "Score: " + value.ToString();
    }
}
