using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public static class GlobalInfo
{
    public static readonly Dictionary<int, BehaviourTreeBlackboardInfo> enemyAbilityInfo = new Dictionary<int, BehaviourTreeBlackboardInfo>()
    {
        {0, BehaviourTreeBlackboardInfo.EnemyAbilityOneReady },
        {1, BehaviourTreeBlackboardInfo.EnemyAbilityTwoReady },
        {2, BehaviourTreeBlackboardInfo.EnemyAbilityThreeReady },
        {3, BehaviourTreeBlackboardInfo.EnemyAbilityFourReady },
        {4, BehaviourTreeBlackboardInfo.EnemyAbilityFiveReady },
        {5, BehaviourTreeBlackboardInfo.EnemyAbilitySixReady },
    };
    public const string enemyTagAndLayer = "Enemy";
    public const string floorTagAndLayer = "Floor";
    public const string obstacleTagAndLayer = "Obstacle";
    public const string tagPlayer = "Player";
    public const string ItemData = "ItemData/";

}



