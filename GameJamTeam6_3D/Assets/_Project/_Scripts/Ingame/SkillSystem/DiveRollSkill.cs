using System;
using System.Collections;
using UnityEngine;

public class DiveRollSkill : Skill
{
    protected override void UseSkill()
    {
        Player.instance.GetStateHandler().SetState(PlayerAnimState.DiveRoll);
    }
}