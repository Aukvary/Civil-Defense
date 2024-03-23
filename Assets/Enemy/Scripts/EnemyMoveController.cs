using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private PlayerTrigger _playerTrigger;
    [SerializeField] private float _freeWalkRange;
    [SerializeField] private Animator _animator;
    
    private Transform _parentTransform;

    private NavMeshAgent _navMeshAgent;

    private ScaleHealth _currentTarget;

    private float _speed;

    private Vector3 basePosition => _parentTransform.position;

    private bool canPickTarget => Vector3.Distance(transform.position, basePosition) < _freeWalkRange;

    public ScaleHealth currentTarget => _currentTarget;

    private void Awake()
    {
        _parentTransform = transform.parent;
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _speed = _navMeshAgent.speed;
    }

    private void Update()
    {
        PickNewTarget();
        _animator.SetBool("HasPath", _navMeshAgent.hasPath);
    }

    private void PickNewTarget()
    {
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            _navMeshAgent.ResetPath();
        _currentTarget = _playerTrigger.GetRandom();

        if(_currentTarget == null || !canPickTarget)
        {
            _navMeshAgent.destination = basePosition;
        }
        else
        {
            _navMeshAgent.SetDestination(_currentTarget.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var stun = other.GetComponent<StunDome>();

        if (stun != null)
            _navMeshAgent.speed = 0;
    }

    private void OnTriggerExit(Collider other)
    {
        var stun = other.GetComponent<StunDome>();

        if (stun != null)
            _navMeshAgent.speed = _speed;
    }
}