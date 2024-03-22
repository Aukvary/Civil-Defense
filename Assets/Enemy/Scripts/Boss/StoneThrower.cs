using UnityEngine;

public class StoneThrower : BossSkill
{
    [SerializeField] private Rigidbody _stown;
    [SerializeField] private float _throwAngle;

    private EnemyMoveController _moveController;

    private ScaleHealth _currentTarget;

    private void Awake()
    {
        _moveController = GetComponentInParent<EnemyMoveController>();
    }

    protected override void UseSkill()
    {
        _currentTarget = _moveController.currentTarget;
        if (_currentTarget == null)
            return;

        var tPos = _currentTarget.transform.position;
        var direction = new Vector3(tPos.x, transform.position.y + 1, tPos.y); 

        float funcX = (direction.x - transform.position.x) / (transform.position - tPos).magnitude;
        float funcZ = (direction.z - transform.position.z) / (transform.position - tPos).magnitude;
        float funcY = (tPos.y - transform.position.y) / (transform.position - tPos).magnitude;
    }
}