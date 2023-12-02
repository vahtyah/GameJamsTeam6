using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAchievement : SerializedMonoBehaviour
{
    [ListDrawerSettings(ShowIndexLabels = true)]
    [SerializeField] PlayerAchievementData playerAchievementData;






}

public class PlayerAchievementData
{
    public string name;
    public string description;
}