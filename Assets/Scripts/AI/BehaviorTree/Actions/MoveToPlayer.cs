using UnityEngine;
using System.Collections.Generic;

public class MoveToPlayer : BehaviorTree
{
    float arrived_distance;

    public override Result Run()
    {
        Vector3 direction = GameManager.Instance.player.transform.position - agent.transform.position;
        if (direction.magnitude < arrived_distance)
        {
            agent.GetComponent<Unit>().movement = new Vector2(0, 0);
            Debug.Log($"[{agent.name}] MoveToPlayer: Arrived at player");
            return Result.SUCCESS;
        }
        else
        {
            agent.GetComponent<Unit>().movement = direction.normalized;
            Debug.Log($"[{agent.name}] MoveToPlayer: Moving towards player");
            return Result.IN_PROGRESS;
        }
    }

    public MoveToPlayer(float arrived_distance) : base()
    {
        this.arrived_distance = arrived_distance;
    }

    public override BehaviorTree Copy()
    {
        return new MoveToPlayer(arrived_distance);
    }
}
