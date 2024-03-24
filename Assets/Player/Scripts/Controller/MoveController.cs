using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(NavMeshAgent))]
class MoveController : MonoBehaviour 
{
    [Header("General")]
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    [Header("Manual")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity;

    [Header("Auto")]
    private Camera _camera;
    private NavMeshAgent _navMeshAgent;

    private bool _isTarget = true;
    
    private List<PlayerControllState> _controls = new List<PlayerControllState>();

    private PlayerControllState _currentControll;

    public new Camera camera
    {
        get => _camera; 
        private set
        {
            if(value != null)
                _camera = value;
        }
    }

    public bool isTarget
    {
        get => _isTarget;

        set
        {
            _isTarget = value;

            foreach ( var control in _controls)
            {
                control.IsTarget = _isTarget;
            }
        }
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.enabled = false;
    }

    private void Start()
    {
        if(GetComponent<PlayerClone>() != null) 
            isTarget = false;

        _controls.Add(new ManualPlayerController(gameObject, _animator, _jumpForce, _gravity));
        _currentControll = _currentControll = _controls[0];

        camera = GetComponentInChildren<CameraController>(true).camera;
        _controls.Add(new AutoPlayerController(gameObject, _animator, _camera));

        foreach (var control in _controls)
        {
            control.speed = _speed;
        }
    }

    private void Update()
    {
        if (!isTarget) return;

        _currentControll.Update();
        ChangeState();
    }

    private void FixedUpdate()
    {
        _currentControll.FixedUpdate();
    }

    private void ChangeState()
    {
        if (!isTarget) return;

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(_navMeshAgent != null && _navMeshAgent.enabled)
                _navMeshAgent.ResetPath();
            _navMeshAgent.enabled = false;
            _currentControll = _controls[0];
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            _animator.SetInteger("RunAnimation", 0);
            _navMeshAgent.enabled = true;
            _currentControll = _controls[1];
        }
    }
}