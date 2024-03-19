using System;

public class CloneHealth : ScaleHealth
{
    public event Action OnDeadEvent;

    public override void Die()
    {
        OnDeadEvent?.Invoke();
        Destroy(gameObject);
    }
}