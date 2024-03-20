using System.Collections.Generic;
using UnityEngine;

public class DarkMirrorSkill : ActionSkill
{
    [SerializeField] private List<float> _cloneLifeTime;
    [SerializeField] private PlayerClone _clone;

    private PlayerClone _currentClone;

    private bool _cloneIstarget;

    private PlayerHealth _mainHealth;
    private LevelController _levelController;

    private CameraController _mainCameraController;
    private CameraController _cloneCameraController;

    private MoveController _mainMoveController;
    private MoveController _cloneMoveController;

    private AttackController _mainAttackController;
    private AttackController _cloneAttackController;

    private ExplosionDomeSkill _explosionDomeSkill;
    private StunDomeSkill _stunDomeSkill;

    private MissDomeSkill _missDomeSkill;
    private UpgradeDomeSkill _upgradeDomeSkill;

    [SerializeField] private GameObject _mainUI;
    private GameObject _cloneUI;

    private float cloneLifeTime => _cloneLifeTime[currentLevel];

    private bool cloneIsTarget
    {
        get => _cloneIstarget;

        set
        {
            _cloneIstarget = value;

            _cloneCameraController.gameObject.SetActive(value);
            _mainCameraController.gameObject.SetActive(!value);

            _cloneMoveController.isTarget = value;
            _mainMoveController.isTarget = !value;

            _cloneAttackController.isTarget = value;
            _mainAttackController.isTarget = !value;

            _stunDomeSkill.IsTaget = value;
            _explosionDomeSkill.IsTaget = !value;

            _upgradeDomeSkill.IsTaget = value;
            _missDomeSkill.IsTaget = !value;

            _cloneUI.SetActive(value);
            _mainUI.SetActive(!value);
        }
    }

    private void Awake()
    {
        _mainHealth = GetComponentInParent<PlayerHealth>();

        _levelController = GetComponent<LevelController>();

        _mainCameraController = GetComponentInChildren<CameraController>();

        _mainMoveController = GetComponentInParent<MoveController>();

        _mainAttackController = GetComponentInChildren<AttackController>();

        _explosionDomeSkill = GetComponent<ExplosionDomeSkill>();

        _missDomeSkill = GetComponent<MissDomeSkill>();
    }

    protected override void Update()
    {
        base.Update();
        ChangeCameraHolder();
    }

    protected override void UseSkill()
    {
        RaycastHit hit;

        if (!Physics.Raycast(viewDirection, out hit)) return;

        _currentClone = _clone.SpawnClone(hit.point + Vector3.up, transform.rotation);

        _currentClone.lifeTime = cloneLifeTime;

        var clonehealth = _currentClone.GetComponentInChildren<CloneHealth>();
        clonehealth.OnDeadEvent += OnDie;
        clonehealth.currentLevel = _mainHealth.currentLevel;

        CloneExpSystem cloneExp = _currentClone.GetComponentInChildren<CloneExpSystem>();
        cloneExp.levelController = _levelController;

        _cloneCameraController = _currentClone.GetComponentInChildren<CameraController>();
        _cloneMoveController = _currentClone.GetComponent<MoveController>();
        _cloneAttackController = _currentClone.GetComponentInChildren<AttackController>();
        _stunDomeSkill = _currentClone.GetComponentInChildren<StunDomeSkill>();
        _upgradeDomeSkill = _currentClone.GetComponentInChildren<UpgradeDomeSkill>();
        _cloneUI = _currentClone.uI;

        _cloneAttackController.currentLevel = _mainAttackController.currentLevel;

        _stunDomeSkill.currentLevel = _explosionDomeSkill.currentLevel;
        _stunDomeSkill.IsTaget = false;

        _upgradeDomeSkill.currentLevel = _missDomeSkill.currentLevel;
        _upgradeDomeSkill.IsTaget = false;

        cloneIsTarget = false;

        Camera.SetupCurrent(_mainCameraController.camera);
    }

    private void OnDie()
    {
        if (cloneIsTarget)
        {
            cloneIsTarget = false;
            _mainCameraController.camera.enabled = true;
            Camera.SetupCurrent(_mainCameraController.camera);
        }
    }

    private void ChangeCameraHolder()
    {
        if (_currentClone == null || !Input.GetKeyDown(KeyCode.Tab))
            return;
        cloneIsTarget = !cloneIsTarget;


        if (cloneIsTarget)
        {
            Camera.SetupCurrent(_mainCameraController.camera);
        }
        else
        {
            Camera.SetupCurrent(_cloneCameraController.camera);
        }

    }
}