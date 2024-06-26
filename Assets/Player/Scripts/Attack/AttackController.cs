using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _spawnerTransform;
    [SerializeField] private float _crit = 1;
    [SerializeField] private int _chance = 1;

    [Header("Stats")]
    [SerializeField] private List<float> _damage;
    [SerializeField] private List<float> _attackSpeed;

    [Header("AutoAttack")]
    [SerializeField] private EnemyTrigger _enemyTrigger;
    [SerializeField] private float _range;

    private bool _isTarget = true;

    private List<AttackState> _states = new List<AttackState>();

    private AttackState _currentState;

    private int _level;

    private NavMeshAgent _navMeshAgent;

    public int currentLevel
    {
        get => _level;

        set
        {
            if (_level < value)
            {
                _level = value;
            }
        }
    }

    public float damage => _damage[_level];

    public float attackSpeed 
    {
        get => _attackSpeed [_level];

        private set
        {
            _attackSpeed[_level] = value;
        }
    }

    public bool isTarget
    {
        get => _isTarget;

        set
        {
            _isTarget = value;

            foreach (var state in _states)
            {
                state.IsTarget = value;
            }
        }
    }

    public float crit
    {
        get => Random.Range(1, 10) % chance == 0 ? _crit : 1;

        set
        {
            if (value < 0) return;
            _crit = value;
        }
    }

    public int chance
    {
        get => _chance;

        set
        {
            if (value < 0) return;
            _crit = value;
        }
    }

    private void Start()
    {
        if (GetComponent<PlayerClone>() != null) isTarget = false;
        var camera = GetComponentInChildren<CameraController>(true).camera;

        var manual = new ManualAttackState(_bullet, _spawnerTransform, camera);
        _states.Add(manual);

        var navMesh = GetComponentInParent<NavMeshAgent>();
        _navMeshAgent = navMesh;
        var auto = new AutoAttackState(_bullet, _spawnerTransform, navMesh, _enemyTrigger, camera);
        auto.range = _range;
        _states.Add(auto);

        _currentState = _states[0];
    }

    private void SetAttackSpeed()
    {
        foreach (var state in _states)
            state.attackSpeed = attackSpeed;
    }

    private void Update()
    {
        SetAttackSpeed();
        ChangeState();
        _currentState.Attack(damage);
    }

    private void ChangeState()
    {
        if (!isTarget) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_navMeshAgent != null && _navMeshAgent.enabled)
            {
                _navMeshAgent.ResetPath();
                _navMeshAgent.enabled = false;
            }
            _enemyTrigger.Clear();
            _currentState = _states[0];
            _enemyTrigger.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _navMeshAgent.enabled = true;
            _currentState = _states[1];
            _enemyTrigger.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var dome = other.GetComponent<UpgradeDome>();

        if (dome != null)
            attackSpeed /= dome.AttackSpeedMulti;
    }

    private void OnTriggerExit(Collider other)
    {
        var dome = other.GetComponent<UpgradeDome>();

        if(dome != null)
            attackSpeed *= dome.AttackSpeedMulti;
    }
}