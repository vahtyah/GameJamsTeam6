using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SkillManager : SerializedMonoBehaviour
{
    public static SkillManager Instance { get; private set; }
    [SerializeField] Dictionary<SkillID, Skill> skills = new Dictionary<SkillID, Skill>();

    private void Awake()
    {
        if(Instance) Destroy(gameObject);
        else Instance = this;
    }

    private void Update()
    {
        if(InputHandler.instance.PressRightClick())
        {
            skills[SkillID.DiveRoll].Use();
        }
    }

    public Skill GetSkill(SkillID _id)
    {
        return skills[_id];
    }
}

public enum SkillID
{
    DiveRoll = 0,
}

