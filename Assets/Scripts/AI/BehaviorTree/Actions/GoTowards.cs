using Unity.VisualScripting;
using UnityEngine;

//add debug
public class GoTowards : BehaviorTree
{
    Transform target;
    float arrived_distance;
    float distance;
    bool in_progress;
    Vector3 start_point;
    Vector3 offset;

    public override Result Run()
    {
        if (!in_progress)
        {
            in_progress = true;
            start_point = agent.transform.position;
        }
        Vector3 direction = (target.position + offset) - agent.transform.position;
        if ((direction.magnitude < arrived_distance) || (agent.transform.position - start_point).magnitude >= distance)
        {
            agent.GetComponent<Unit>().movement = new Vector2(0, 0);
            in_progress = false;
            return Result.SUCCESS;
        }
        else
        {
            agent.GetComponent<Unit>().movement = direction.normalized;
            return Result.IN_PROGRESS;  
        }
    }

    public GoTowards(Transform target, float distance, float arrived_distance) : base()
    {
        this.target = target;
        this.arrived_distance = arrived_distance;
        this.distance = distance;
        this.in_progress = false;
        this.offset = new Vector3(Random.Range(-0.75f, 0.75f), 0, Random.Range(-0.75f, 0.75f));
    }

    public override BehaviorTree Copy()
    {
        return new GoTowards(target, distance, arrived_distance);
    }
}

