using System;
using UnityEngine;

public abstract class Skill
{
    [SerializeField] protected SkillSO skillData;
    private float nextUseTime;
    public Action OnSkillUsed;

    public void Use()
    {
        if (!(Time.time > nextUseTime)) return;
        UseSkill();
        OnSkillUsed?.Invoke();
        nextUseTime = Time.time + skillData.cooldownTime;
    }

    protected abstract void UseSkill();
    public SkillSO GetSkillData() => skillData;
}