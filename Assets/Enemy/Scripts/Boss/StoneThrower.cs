using UnityEngine;

public class StoneThrower : BossSkill
{
    [SerializeField] private float _throwSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private ThrowStone _stone;
    [SerializeField] private Transform _spawTransform;

    protected override void UseSkill()
    {
        var _currentTarget = moveController.currentTarget;
        if (_currentTarget == null)
            return;

        _stone.Shoot(_spawTransform.position, _currentTarget.transform.position + Vector3.up * 2, _throwSpeed, _damage);
    }
}