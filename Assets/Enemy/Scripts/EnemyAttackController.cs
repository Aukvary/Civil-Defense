using System;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _angleAttack;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyAttackEvent _onAttackEvent;

    private EnemyMoveController _moveController;

    private ScaleHealth health => _moveController.currentTarget;

    private void Awake()
    {
        _moveController = transform.parent.GetComponent<EnemyMoveController>();
    }
    private void OnEnable()
    {
        _onAttackEvent.OnAttackEvent += Attack;
    }
    private void OnDisable()
    {
        _onAttackEvent.OnAttackEvent -= Attack;
    }

    private void Update()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        if (health == null)
            return;
        var distance = Vector3.Distance(transform.position, health.transform.position);
        var angle = Vector3.Angle(transform.forward, health.transform.position - transform.position);

        if(distance < _range && angle < _angleAttack)
            _animator.SetTrigger("Attack");
    }

    private void Attack()
    {
        if (health == null)
            return;
        var distance = Vector3.Distance(transform.position, health.transform.position);
        var angle = Vector3.Angle(transform.forward, health.transform.position - transform.position);

        if (distance < _range && angle < _angleAttack)
            health?.DealDamage(_damage);
    }
}