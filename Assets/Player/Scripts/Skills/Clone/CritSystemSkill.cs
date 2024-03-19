using System.Collections.Generic;
using UnityEngine;

class CritSystemSkill : Skill
{
    [SerializeField] private List<float> _crit;
    [SerializeField] private List<float> _chance;

    private AttackController _attackController;

    private void Awake()
    {
        _attackController = GetComponent<AttackController>();
    }

    protected override void UseSkill()
    {
        if(_attackController.crit != _crit[currentLevel])
            _attackController.crit = _crit[currentLevel];
        if(_attackController.chance != _chance[currentLevel])
            _attackController.crit = _chance[currentLevel];
    }
}