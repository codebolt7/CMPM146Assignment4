using UnityEngine;
using System.Collections.Generic;

// kinda have no idea what i was doing here but trying to make 
public class FollowEnemy : BehaviorTree
{
    private float followDist;
    private bool in_progress = false;

    public FollowEnemy(float followDist) : base()
    {
        this.followDist = followDist;
    }

    public override Result Run()
    {
        // essentially was trying to implement an action that accesses a "FollowTarget" from the blackboard and has the enemy follow that enemy -> use for skeletons and warlocks ? 
    }

    public override BehaviorTree Copy()
    {
        return new FollowEnemy(followDist);
    }
}

