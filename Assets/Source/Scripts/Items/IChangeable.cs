
using System;

public interface IChangeable
{
    event Action<float> ValueChanged;

    void OnPicked(float value);
}