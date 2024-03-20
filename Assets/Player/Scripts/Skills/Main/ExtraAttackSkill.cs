using System.Collections.Generic;
using UnityEngine;

class ExtraAttackSkill : Skill
{
    [Header("Stats")]
    [SerializeField] private List<float> _damage;
    [SerializeField] private List<float> _coolDown;
    [SerializeField] private List<int> _count;

    [Header("Links")]
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private EnemyTrigger _enemyTrigger;

    private float _currentCoolDownTime;

    protected override void UseSkill()
    {
        if (_currentCoolDownTime < _coolDown[currentLevel])
        {
            _currentCoolDownTime += Time.deltaTime;
            return;
        }
        _currentCoolDownTime = 0;
        Shoot();
    }

    private void Shoot()
    {
        if (_enemyTrigger.objects == null)
            return;
        int count = _count[currentLevel];
        for(int i = _enemyTrigger.objects.Count - 1; i >= 0; i--)
        {
            if (count < 0) return;
            if (_enemyTrigger.objects[i] == null)
            {
                _enemyTrigger.objects.Remove(_enemyTrigger.objects[i]);
                continue;
            }

            _spawnPosition.LookAt(_enemyTrigger.objects[i].transform.position);
            _bullet.Shoot(_spawnPosition, _damage[currentLevel]);
            count--;
        }
    }
}