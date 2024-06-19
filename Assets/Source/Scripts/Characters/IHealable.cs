using System;

public interface IHealable
{
    event Action<float> Healed;
}