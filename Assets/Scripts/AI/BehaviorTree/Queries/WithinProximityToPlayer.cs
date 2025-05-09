using System.Collections.Generic;
using UnityEngine;


public class WithinProximityToPlayer : BehaviorTree
{
    float distanceThreshold;

    public override Result Run()
    {
        var distance = 
            (GameManager.Instance.player.transform.position - agent.transform.position).magnitude;
        return distance < distanceThreshold ? Result.SUCCESS : Result.FAILURE;
    }

    public WithinProximityToPlayer(float distanceThreshold) : base()
    {
        this.distanceThreshold = distanceThreshold;
    }

    public override BehaviorTree Copy()
    {
        return new WithinProximityToPlayer(distanceThreshold);
    }
}
