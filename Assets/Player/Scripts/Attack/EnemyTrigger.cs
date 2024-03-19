using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private List<EnemyHealth> _enemyHealths = new List<EnemyHealth>();

    public List<EnemyHealth> enemyHealths => _enemyHealths;

    public int count => _enemyHealths.Count;

    public EnemyHealth GetRandom()
    {
        if (_enemyHealths.Count == 0) return null;

        return _enemyHealths[Random.Range(0, _enemyHealths.Count - 1)];
    }

    public void Clear() =>
        _enemyHealths.Clear();

    private void OnTriggerEnter(Collider other)
    {
        var healthController = other.GetComponent<EnemyHealth>();

        if (healthController == null) return;

        _enemyHealths.Add(healthController);
    }

    private void OnTriggerExit(Collider other)
    {
        var enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth == null) return;

        _enemyHealths.Remove(enemyHealth);
    }
}