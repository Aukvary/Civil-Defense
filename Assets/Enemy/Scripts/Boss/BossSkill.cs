using UnityEngine;

public abstract class BossSkill : MonoBehaviour
{
    [SerializeField] private PlayerTrigger _trigger;
    [SerializeField] private float _coolDownTime;

    private float _currentCoolDownTime;

    public PlayerTrigger trigger => _trigger;
    public float coolDownTime => _coolDownTime;

    private void Update()
    {
        if(_currentCoolDownTime < coolDownTime)
        {
            _currentCoolDownTime += Time.deltaTime;
            return;
        }
        _currentCoolDownTime = 0;
        UseSkill();
    }

    protected abstract void UseSkill();
}