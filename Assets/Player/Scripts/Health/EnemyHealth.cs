using System;
using UnityEngine;

public class EnemyHealth : HealthController
{
    [SerializeField] private float _health;

    private float _maxHealth;

    public event Action OnDieEvent;

    public event Action OnHitEvent;

    public override float health 
    { 
        get => _health;
        protected set
        {
            _health = value;
            _health = Math.Clamp(_health, 0, _maxHealth);
            if (health <= 0)
                Die();
        }
    }

    private void Awake()
    {
        _maxHealth = _health;
    }

    public override void DealDamage(float damage)
    {
        base.DealDamage(damage);
        OnHitEvent?.Invoke();
    }

    public override void Die()
    {
        OnDieEvent?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        var bullet = col.GetComponent<Bullet>();
        var explosion = col.GetComponent<ExplosionDome>();

        if (bullet != null)
        {
            DealDamage(bullet.damage);
        }

        if(explosion != null)
        {
            DealDamage(explosion.damage);
            explosion.DestroyDome();
        }
    }
}