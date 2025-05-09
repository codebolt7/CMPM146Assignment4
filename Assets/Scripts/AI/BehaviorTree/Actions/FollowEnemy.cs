using UnityEngine;
using System.Collections.Generic;


public class FollowEnemy : BehaviorTree
{
    private float followDist;

    public FollowEnemy(float followDist) : base()
    {
        this.followDist = followDist;
    }

    public override Result Run()
    {
        // implements an action that accesses a "FollowTarget" from the blackboard and has the enemy follow that enemy
        // used for skeletons and warlocks

        var target = GetBBEnemy("FollowTarget");

        if (target == null) return Result.FAILURE;

        Vector3 direction = target.transform.position - agent.transform.position;
        if (direction.magnitude < followDist)
        {
            agent.GetComponent<Unit>().movement = new Vector2(0, 0);
            return Result.SUCCESS;
        }
        else
        {
            agent.GetComponent<Unit>().movement = direction.normalized;
            return Result.IN_PROGRESS;
        }
    }

    public override BehaviorTree Copy()
    {
        return new FollowEnemy(followDist);
    }
}

