using System;
using UnityEngine;

public class PlayerClone : MonoBehaviour 
{
    [SerializeField] private GameObject _uI;

    public GameObject uI => _uI;
    private float _lifeTime;

    private float _currentLifeTime;

    private MoveController _moveController;

    private CameraController _cameraController;

    private CloneHealth _health;

    public float lifeTime
    {
        get => _lifeTime;

        set
        {
            if (value > 0) _lifeTime = value;
        }
    }

    private void Awake()
    {
        _moveController = GetComponent<MoveController>();
        _moveController.isTarget = false;
        _cameraController = GetComponentInChildren<CameraController>(true);

        _health = GetComponent<CloneHealth>();
        uI.SetActive(false);
    }

    private void Update()
    {
        CheckALife();
    }
    private void CheckALife()
    {
        if (_lifeTime > _currentLifeTime)
        {
            _currentLifeTime += Time.deltaTime;
        }
        else
        {
            _health.Die();
        }
    }
    public PlayerClone SpawnClone(Vector3 position, Quaternion rotation)
    {
        var obj = Instantiate(gameObject ,position, rotation);

        obj.GetComponent<MoveController>().isTarget = false;

        return obj.GetComponent<PlayerClone>();
    }
}