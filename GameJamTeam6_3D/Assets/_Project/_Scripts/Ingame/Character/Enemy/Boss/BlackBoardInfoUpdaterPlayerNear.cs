using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackBoardInfoUpdaterPlayerNear : MonoBehaviour
{
    [SerializeField] float belowDistance = 1f;
    BehaviourTreeBossBlackboard blackboard;

    private void Awake()
    {
        blackboard = GetComponent<BehaviourTreeBossBlackboard>();
        belowDistance = Mathf.Sqrt(belowDistance);
    }

    void Update()
    {
        if (Time.frameCount % 4 == 0)
        {
            blackboard.AssignBlackBoard(BehaviourTreeBlackboardInfo.NearPlayer
                , () => (Player.instance.transform.position - transform.position).sqrMagnitude < belowDistance
                );
        }
    }
}
