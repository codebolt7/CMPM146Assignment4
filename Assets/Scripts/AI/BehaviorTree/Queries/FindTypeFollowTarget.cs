using UnityEngine;

public class FindTypeFollowTarget : BehaviorTree
{
    string monster;
    float maxDistance;

    public override Result Run()
    {
        var possibleTargets = GameManager.Instance.GetEnemiesInRange(agent.transform.position, maxDistance);

        EnemyController bestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject e in possibleTargets)
        {
            var enemy = e.GetComponent<EnemyController>();
            if (enemy == null) continue;

            float dist = (agent.transform.position - e.transform.position).magnitude;

            if (enemy.monster == monster && dist < closestDistance)
            {
                bestTarget = enemy;
                closestDistance = dist;
                return Result.SUCCESS;
            }
        }
        
        SetBBEnemy("FollowTarget", bestTarget);

        return Result.FAILURE;
    }


    /// <summary>
    /// Follows the specified Monster type
    /// </summary>
    /// <param name="monster"></param>
    /// <param name="maxDistance"></param>
    public FindTypeFollowTarget(string monster, float maxDistance = Mathf.Infinity) : base()
    {
        this.monster = monster;
        this.maxDistance = maxDistance;
    }

    public override BehaviorTree Copy()
    {
        return new FindTypeFollowTarget(monster, maxDistance);
    }
}
