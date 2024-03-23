using UnityEngine;

public class StoneRing : BossSkill
{
    [SerializeField] private ThrowStone _stone;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime;

    protected override void UseSkill()
    {
        var target = moveController.currentTarget;
        if (target == null)
            return;

        float distance = Vector3.Distance(target.transform.position, transform.position) + 2;
        for (int i = 0; i < 360; i += 10)
        {
            var pos = new Vector3(distance * Mathf.Cos(i), transform.position.y + 1, distance * Mathf.Sin(i));
            _stone.Spawn(transform, pos, _damage, _lifeTime);
        }
    }
}