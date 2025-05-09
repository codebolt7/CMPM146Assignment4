using UnityEngine;
using System.Collections.Generic;

public class GoTo : BehaviorTree
{
    Transform target;
    float arrived_distance;
    Vector3 offset;

    public override Result Run()
    {
        Vector3 direction = (target.position + offset) - agent.transform.position;
        if (direction.magnitude < arrived_distance)
        {
            agent.GetComponent<Unit>().movement = new Vector2(0, 0);
            //Debug.Log($"[{agent.monster}] GoTo: Arrived at location");
            return Result.SUCCESS;
        }
        else
        {
            agent.GetComponent<Unit>().movement = direction.normalized;
            return Result.IN_PROGRESS;
        }
    }

    public GoTo(Transform target, float arrived_distance) : base()
    {
        this.target = target;
        this.arrived_distance = arrived_distance;
        this.offset = new Vector3(Random.Range(-0.75f, 0.75f), 0, Random.Range(-0.75f, 0.75f));
    }

    public override BehaviorTree Copy()
    {
        return new GoTo(target, arrived_distance);
    }
}

