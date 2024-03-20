using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _crip;
    [SerializeField] private int _count;
    [SerializeField] private float _coolDownTime;

    private float _currentTime;

    private List<HealthController> _entities;

    private bool canSpawn => _entities.Count == 0;

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
        for(int i = _entities.Count - 1; i >= 0; i--)
        {
            if(_entities[i] == null)
                _entities.RemoveAt(i);
        }
        if (!canSpawn)
            return;

        for(int i = 0; i < _count; i++)
        {
            Instantiate(_crip, transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<HealthController>();

        if(health != null)
            _entities.Add(health);
    }

    private void OnTriggerExit(Collider other)
    {
        var health = other.GetComponent<HealthController>();

        if (health != null)
            _entities.Remove(health);
    }
}