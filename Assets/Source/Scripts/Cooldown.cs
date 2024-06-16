using System;
using System.Threading.Tasks;

public class Cooldown
{
    private readonly float _seconds;

    public Cooldown(float seconds)
    {
        _seconds = seconds;

        IsPassed = true;
    }

    public bool IsPassed { get; private set; }

    public void Start()
    {
        if (IsPassed)
        {
            Task task = PassTime();
        }
    }

    private async Task PassTime()
    {
        IsPassed = false;

        await Task.Delay(TimeSpan.FromSeconds(_seconds));

        IsPassed = true;
    }
}
