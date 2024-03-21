using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

class AutoAttackState : AttackState
{
    private NavMeshAgent _navMeshAgent;

    private EnemyTrigger _trigger;

    private EnemyHealth _enemyHealth;

    private Camera _camera;

    private float _range;

    private Ray viewDirection => _camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));

    public float range
    {
        get => _range; 
        set 
        {
            if (value > 0) 
                _range = value;
        }
    }

    public AutoAttackState(Bullet bullet, Transform spawnerTransform, 
        NavMeshAgent navMeshAgent, EnemyTrigger trigger, Camera camera) : 
        base(bullet, spawnerTransform)
    {
        _navMeshAgent = navMeshAgent;
        _trigger = trigger;
        _camera = camera;
    }

    public override void Attack(float damage)
    {
        SetTimer();
        ManualPickEnemy();
        if (_enemyHealth == null) 
            _enemyHealth = _trigger.GetRandom();

        if (_enemyHealth == null) 
            return;

        FollowToEnemy();
        spawnerTransform.LookAt(_enemyHealth.transform);
        Shoot(damage);
    }

    private void ManualPickEnemy()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0) || !IsTarget) return;

        RaycastHit hit;

        if (!Physics.Raycast(viewDirection, out hit)) return;

        var health = hit.collider.GetComponent<EnemyHealth>();

        if (health != null)
            _enemyHealth = health;
    }


    private void FollowToEnemy()
    {
        if( Vector3.Distance(_enemyHealth.transform.position, spawnerTransform.position) <= range)
        {
            _navMeshAgent.ResetPath();
            return;
        }
        if(_enemyHealth == null) return;
        _navMeshAgent.destination = _enemyHealth.transform.position;
    }

    public void Stop()
    {
        _enemyHealth = null;
    }
}