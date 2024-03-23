using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _crip;
    [SerializeField] private int _count;
    [SerializeField] private float _coolDownTime;

    private float _currentTime;

    private bool canSpawn
    {
        get
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f);

            foreach (Collider collider in colliders)
            {
                if(collider.GetComponent<HealthController>())
                {
                    return false;
                }
            }
            return true;
        }
    }

    private void Start()
    {
        _currentTime = _coolDownTime;
    }

    private void Update()
    {
        if(_currentTime < _coolDownTime)
        {
            _currentTime += Time.deltaTime;
            return;
        }
        Spawn();
        _currentTime = 0;

    }

    private void Spawn()
    {
        if (!canSpawn)
            return;

        for(int i = 0; i < _count; i++)
        {
            Instantiate(_crip, transform);
        }
    }
}