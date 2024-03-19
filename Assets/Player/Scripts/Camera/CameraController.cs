using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float _rotattionSpeed;
    [SerializeField] private Camera _camera;

    [Header("FreeCamera")]
    [SerializeField] private float _minFreeAngle;
    [SerializeField] private float _maxFreeAngle;

    [Header("FiexedCamera")]
    [SerializeField] private float _minFixedAngle;
    [SerializeField] private float _maxFixedAngle;

    public new Camera camera => _camera;

    private List<CameraState> _states = new List<CameraState>();

    private CameraState _currentState;

    private void Awake()
    {
        var fixedCam = new FixedCameraState(gameObject, _rotattionSpeed);
        fixedCam.SetAngles(_minFixedAngle, _maxFixedAngle);
        _states.Add(fixedCam);

        var freeCam = new FreeCameraState(gameObject, _rotattionSpeed);
        freeCam.SetAngles(_minFreeAngle, _maxFreeAngle);
        _states.Add(freeCam);

        _currentState = _states[0];
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        ChangeState();
        _currentState.Rotate();
    }

    private void ChangeState()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentState = _states[0];
            transform.parent = _currentState.followObject;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentState = _states[1];
            transform.parent = null;
        }
    }

    public void ChangeState(int state)
    {
        switch (state)
        {
            case 0:
                _currentState = _states[0];
                transform.parent = _currentState.followObject;
                break;

            case 1:
                _currentState = _states[1];
                transform.parent = null;
                break;

            default:
                _currentState = _states[1];
                transform.parent = null;
                break;
        }
    }
}
