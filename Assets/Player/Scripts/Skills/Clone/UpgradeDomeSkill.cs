using System.Collections.Generic;
using UnityEngine;

public class UpgradeDomeSkill : SpawnSkill
{
    [SerializeField] private List<float> _attackSpeedMulti;

    protected override void UseSkill()
    {
        base.UseSkill();

        (dome as UpgradeDome).AttackSpeedMulti = _attackSpeedMulti[currentLevel];
    }
}
