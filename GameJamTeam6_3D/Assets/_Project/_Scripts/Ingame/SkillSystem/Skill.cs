using UnityEngine;

public abstract class Skill
{
    [SerializeField] protected float cooldownTime;
    private float nextUseTime;

    public void Use()
    {
        if (!(Time.time > nextUseTime)) return;
        UseSkill();
        nextUseTime = Time.time + cooldownTime;
    }

    protected abstract void UseSkill();
}