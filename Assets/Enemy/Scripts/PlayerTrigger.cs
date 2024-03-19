using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private List<ScaleHealth> _playerHealths = new List<ScaleHealth>();

    public List<ScaleHealth> playerHealths => _playerHealths;

    public int count => _playerHealths.Count;

    public ScaleHealth GetRandom()
    {
        if (_playerHealths.Count == 0) return null;

        return _playerHealths[Random.Range(0, _playerHealths.Count - 1)];
    }

    public void Clear() =>
        _playerHealths.Clear();

    private void OnTriggerEnter(Collider other)
    {
        var healthController = other.GetComponent<ScaleHealth>();

        if (healthController == null) return;

        _playerHealths.Add(healthController);
    }

    private void OnTriggerExit(Collider other)
    {
        var healthController = other.GetComponent<ScaleHealth>();

        if (healthController == null) return;

        _playerHealths.Remove(healthController);
    }
}