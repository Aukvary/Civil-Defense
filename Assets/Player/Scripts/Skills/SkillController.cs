using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private List<BaseSkill> _skills;

    private void Awake()
    {
        _skills = new List<BaseSkill>();

        var skills = Resources.LoadAll<BaseSkill>("");

        foreach(var skill in skills)
            _skills.Add(skill);
        print(_skills.Count);
    }
}
