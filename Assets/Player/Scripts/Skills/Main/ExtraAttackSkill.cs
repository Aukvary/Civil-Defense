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
        if (_enemyTrigger.enemyHealths == null)
            return;
        int count = _count[currentLevel];
        foreach(var mob in _enemyTrigger.enemyHealths)
        {
            if (count < 0) return;
            if (mob == null)
            {
                _enemyTrigger.enemyHealths.Remove(mob);
                return;
            }

            _spawnPosition.LookAt(mob.transform.position);
            _bullet.Shoot(_spawnPosition, _damage[currentLevel]);
            count--;
        }
    }
}