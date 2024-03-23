using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : HealthController
{
    [SerializeField] private float _health;
    [SerializeField] private float _disappearSpeed;
    [SerializeField] private Animator _animator;

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
        _animator.SetTrigger("Die");
        transform.parent.GetComponent<EnemyMoveController>().enabled = false;
        transform.parent.GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyAttackController>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        Destroy(transform.parent.gameObject, 3);
    }

    private void OnTriggerEnter(Collider col)
    {
        var bullet = col.GetComponent<Bullet>();
        var explosion = col.GetComponent<ExplosionDome>();

        if (bullet != null)
        {
            DealDamage(bullet.damage);
        }

        if (explosion != null)
        {
            DealDamage(explosion.damage);
            explosion.DestroyDome();
        }
    }
}