using System.Collections.Generic;
using UnityEngine;

public class SpawnSkill : ActionSkill
{
    [SerializeField] private Dome _dome;

    [SerializeField] private List<float> _lifeTime;

    [SerializeField] private int _skillCost;

    private HealthController _healthControlller;

    private Dome _currentDome;

    protected Dome dome
    {
        get => _currentDome;

        private set => _currentDome = value;
    }

    private float lifeTime => _lifeTime[currentLevel];

    private void Awake()
    {
        if (GetComponent<PlayerClone>())
            _healthControlller = GetComponent<CloneHealth>();
        else
            _healthControlller = GetComponent<PlayerHealth>();

    }

    protected override void UseSkill()
    {
        RaycastHit hit;
        if (!Physics.Raycast(viewDirection, out hit)) return;

        _healthControlller.DealDamage(_skillCost);
        dome = _dome.Create(hit.point, lifeTime);
    }
}