using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AttackState
{
    public bool IsTarget = true;

    private float _damage;

    private float _attackSpeed;

    private Bullet _bullet;

    private Transform _spawnerTransform;

    private float _currentCoolDown = 0;

    public float damage
    {
        get => _damage;

        private set => _damage = value;
    }

    public float attackSpeed
    {
        get => _attackSpeed;

        set
        {
            if (value > 0)
                _attackSpeed = value;
        }
    }

    protected Bullet bullet
    {
        get => _bullet;

        private set => _bullet = value;
    }

    protected Transform spawnerTransform
    {
        get => _spawnerTransform;

        private set => _spawnerTransform = value;
    }

    public AttackState(Bullet bullet, Transform spawnerTransform)
    {
        _bullet = bullet;

        _spawnerTransform = spawnerTransform;
    }


    protected void SetTimer()
    {
        if (_currentCoolDown < attackSpeed)
            _currentCoolDown += Time.deltaTime;            
    }

    protected void Shoot(float damage)
    {
        if (_currentCoolDown < attackSpeed)
            return;

        _bullet.Shoot(_spawnerTransform, damage);
        _currentCoolDown = 0;
    }
    public abstract void Attack(float damage);
}
