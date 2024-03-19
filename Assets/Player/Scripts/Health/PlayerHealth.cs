using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerHealth : ScaleHealth
{
    [Header("DeathParametors")]
    [SerializeField] private float _respawnTime;
    [SerializeField] private Transform _respawnPosition;

    private DarkMirrorSkill _darkMirrorSkill;
    private ExplosionDomeSkill _explosionDomeSkill;
    private MoveController _moveController;
    private AttackController _attackController;
    private NavMeshAgent _navMeshAgent;
    private CharacterController _characterController;
    private CameraController _cameraController;

    private bool _isAlive = true;

    private float _currentRespawnTime;

    public bool isAlive
    {
        get => _isAlive;

        private set
        {
            _isAlive = value;
            if (_navMeshAgent.hasPath)
                _navMeshAgent.ResetPath();

            _darkMirrorSkill.enabled = value;
            _explosionDomeSkill.enabled = value;
            _moveController.enabled = value;
            _attackController.enabled = value;
            _navMeshAgent.enabled = value;
            _characterController.enabled = value;

            _cameraController.ChangeState(_isAlive ? 0 : 1);

            DrawUI();
        }
    }

    private void Awake()
    {
        _darkMirrorSkill = GetComponent<DarkMirrorSkill>();
        _explosionDomeSkill = GetComponent<ExplosionDomeSkill>();
        _moveController = GetComponent<MoveController>();
        _attackController = GetComponent<AttackController>();
        _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        _characterController = GetComponent<CharacterController>();
        _cameraController = GetComponentInChildren<CameraController>();
    }

    protected override void Update()
    {
        base.Update();
        Rehabilitation();
        if (Input.GetKeyDown(KeyCode.J))
        {
            DealDamage(10);
        }
    }

    private void Rehabilitation()
    {
        if (!isAlive && _currentRespawnTime < _respawnTime)
        {
            _currentRespawnTime += Time.deltaTime;
        }
        else if (_currentRespawnTime > _respawnTime)
        {
            _currentRespawnTime = 0;
            OnAlive();
        }
    }
    public override void Die()
    {
        isAlive = false;
        transform.position = _respawnPosition.position;
    }

    private void OnAlive()
    {
        isAlive = true;
    }
}
