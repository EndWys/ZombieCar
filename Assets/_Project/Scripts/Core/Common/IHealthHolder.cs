using System;

public interface IHealthHolder
{

    public event Action OnHealthGone;
    public event Action OnHealthChanged;

    public int CurrentHealth { get; }
    public int MaxHealth { get; }

    public void ResetHealth();
}
