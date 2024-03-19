using System.Collections.Generic;
using UnityEngine;

public class ExplosionDomeSkill : SpawnSkill
{
    [SerializeField] private List<int> _damage;

    protected override void UseSkill()
    {
        base.UseSkill();

        (dome as ExplosionDome).damage = _damage[currentLevel];
    }
}