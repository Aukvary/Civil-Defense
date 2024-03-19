using UnityEngine;

public abstract class HealthController : MonoBehaviour
{
    [SerializeField] private float _regeneration;

    protected float regeneration
    {
        get => _regeneration;
        set
        {
            if(value > 0)
                _regeneration = value;
        }
    }

    public abstract float health {get; protected set; }

    protected virtual void Update()
    {
        health += regeneration * Time.deltaTime;
    }

    public virtual void DealDamage(float damage)
    {
        if (damage < 0) return;
        
        health -= damage;
    }

    public abstract void Die();
}