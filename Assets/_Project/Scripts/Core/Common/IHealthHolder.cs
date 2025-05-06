using System;

public interface IHealthHolder
{

    public event Action OnHealthGone;

    public void ResetHealth();
}
