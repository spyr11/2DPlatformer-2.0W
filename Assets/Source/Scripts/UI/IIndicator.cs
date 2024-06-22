
using System;

public interface IIndicator
{
    event Action<float> Changed;

    float MaxValue { get; }
}
