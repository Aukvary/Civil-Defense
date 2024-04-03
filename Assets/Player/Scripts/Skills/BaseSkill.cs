using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skills")]
public class BaseSkill : ScriptableObject
{
    [SerializeField] private KeyCode _skillKey;

    protected void UseSkill()
    {

    }
}
