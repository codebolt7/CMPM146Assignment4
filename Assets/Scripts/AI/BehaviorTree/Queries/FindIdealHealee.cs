using UnityEngine;
using System.Collections.Generic;

public class FindIdealHealee : BehaviorTree
{
    private float range;
    private int threshold;

    public override Result Run()
    {
        EnemyAction act = agent.GetAction("heal");
        if (act == null) return Result.FAILURE;

        List<GameObject> possibleTargets = GameManager.Instance.GetEnemiesInRange(agent.transform.position, range);
        if (possibleTargets.Count == 0) return Result.FAILURE;

        EnemyController target = null;
        float lowestHealthPercentage = 1f;

        // Check for lowest enemy health
        foreach (GameObject t in possibleTargets)
        {
            EnemyController enemy = t.GetComponent<EnemyController>();
            if (enemy == null) continue;

            // ignore if warlock bc warlocks cannot be healed
            if (enemy.monster == "warlock") continue; 

            // Ignore if missing HP amount is below threshold
            if (enemy.hp.max_hp - enemy.hp.hp < threshold) continue;

            float healthPercentage = enemy.hp.hp / enemy.hp.max_hp;
            if (healthPercentage < lowestHealthPercentage)
            {
                lowestHealthPercentage = healthPercentage;
                target = enemy;
            }
        }

        if (target != null)
        {
            SetBBEnemy("IdealHealee", target);
        }

        return target == null ? Result.FAILURE : Result.SUCCESS;
    }


    /// <summary>
    /// Searches for the most ideal enemy in range to heal 
    /// and adds it to the blackboard under the key "IdealHealee".
    /// </summary>
    /// <param name="range">Range of the heal ability</param>
    /// <param name="threshold">Lowest amount of health missing to be viable</param>
    public FindIdealHealee(float range, float threshold = 10f) : base()
    {
        this.range = range;
    }

    public override BehaviorTree Copy()
    {
        return new FindIdealHealee(range, threshold);
    }
}
