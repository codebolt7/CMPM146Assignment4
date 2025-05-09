using UnityEngine;
using System.Collections.Generic;

public class HealIdeal : BehaviorTree
{
    public override Result Run()
    {
        var target = GetBBEnemy("IdealHealee");
        if (target == null) return Result.FAILURE;

        EnemyAction act = agent.GetAction("heal");
        if (act == null) return Result.FAILURE;

        bool success = act.Do(target.transform);
        Debug.Log($"[{agent.monster}] HealIdeal: Heal {(success ? "succeeded" : "failed")} on target {target.monster}.");
        return (success ? Result.SUCCESS : Result.FAILURE);

    }

    public HealIdeal() : base()
    {

    }

    public override BehaviorTree Copy()
    {
        return new HealIdeal();
    }
}
