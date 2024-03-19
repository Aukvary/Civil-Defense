using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [Range(0, 3)] private int _currentSkillLevel = 0;
    [SerializeField] private KeyCode _actionKey;

    public KeyCode actionKey => _actionKey;

    public int currentLevel
    {
        get => _currentSkillLevel;
        set
        {
            if (_currentSkillLevel < value && value <= 3)
            {
                _currentSkillLevel = value;
            }
        }
    }

    protected virtual void Update()
    {
        UseSkill();
    }

    protected abstract void UseSkill();
}