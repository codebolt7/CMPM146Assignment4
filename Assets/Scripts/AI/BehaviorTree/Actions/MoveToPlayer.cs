using UnityEngine;
using System.Collections.Generic;

public class MoveToPlayer : BehaviorTree
{
    float arrived_distance;
    Vector3 offset;

    public override Result Run()
    {
        Vector3 direction = (GameManager.Instance.player.transform.position + offset) - agent.transform.position;
        if (direction.magnitude < arrived_distance)
        {
            agent.GetComponent<Unit>().movement = new Vector2(0, 0);
            Debug.Log($"[{agent.monster}] MoveToPlayer: Arrived at player");
            return Result.SUCCESS;
        }
        else
        {
            agent.GetComponent<Unit>().movement = direction.normalized;
            return Result.IN_PROGRESS;
        }
    }

    public MoveToPlayer(float arrived_distance) : base()
    {
        this.arrived_distance = arrived_distance;
        this.offset = new Vector3(Random.Range(-0.75f, 0.75f), 0, Random.Range(-0.75f, 0.75f));
    }

    public override BehaviorTree Copy()
    {
        return new MoveToPlayer(arrived_distance);
    }
}
