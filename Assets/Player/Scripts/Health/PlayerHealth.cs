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
    [Header("UI")]
    [SerializeField] private GameObject _deathUI;

    private ExplosionDomeSkill _explosionDomeSkill;
    private MissDomeSkill _missDomeSkill;
    private ExtraAttackSkill _extraAttackSkill;
    private DarkMirrorSkill _darkMirrorSkill;
    private MoveController _moveController;
    private AttackController _attackController;
    private NavMeshAgent _navMeshAgent;
    private CharacterController _characterController;
    private CameraController _cameraController;

    private LevelController _levelController;

    private bool _isAlive = true;

    private float _currentRespawnTime;

    public bool isAlive
    {
        get => _isAlive;

        private set
        {
            _isAlive = value;
            if (_navMeshAgent.enabled && _navMeshAgent.hasPath)
                _navMeshAgent.ResetPath();
            if (_levelController._enabled[0])
                _explosionDomeSkill.enabled = value;
            if (_levelController._enabled[1])
                _darkMirrorSkill.enabled = value;
            if (_levelController._enabled[2])
                _moveController.enabled = value;
            if (_levelController._enabled[3])
                _attackController.enabled = value;
            _navMeshAgent.enabled = false;
            _characterController.enabled = value;
            _deathUI.SetActive(!value);

            _cameraController.ChangeState(_isAlive ? 0 : 1);

            DrawUI();
        }
    }

    private void Awake()
    {
        _explosionDomeSkill = GetComponentInChildren<ExplosionDomeSkill>();
        _missDomeSkill = GetComponentInChildren<MissDomeSkill>();
        _extraAttackSkill = GetComponentInChildren<ExtraAttackSkill>();
        _darkMirrorSkill = GetComponentInChildren<DarkMirrorSkill>();
        _moveController = GetComponent<MoveController>();
        _attackController = GetComponentInChildren<AttackController>();
        _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        _characterController = GetComponent<CharacterController>();
        _cameraController = GetComponentInChildren<CameraController>();

        _levelController = GetComponentInChildren<LevelController>();
    }

    protected override void DrawUI()
    {
        base.DrawUI();
        var timer = _deathUI.GetComponentInChildren<TextMeshProUGUI>();
        timer.text = ((int)(_respawnTime - _currentRespawnTime)).ToString();
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
            DrawUI();
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
