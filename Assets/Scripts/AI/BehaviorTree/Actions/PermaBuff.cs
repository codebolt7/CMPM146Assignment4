using UnityEngine;
using System.Collections.Generic;

public class PermaBuff : BehaviorTree
{
    public override Result Run()
    {
        var target = GameManager.Instance.GetClosestOtherEnemy(agent.gameObject);
        EnemyAction act = agent.GetAction("permabuff");
        if (act == null) return Result.FAILURE;

        bool success = act.Do(target.transform);
        Debug.Log($"[{agent.name}] PermaBuff: Buff {(success ? "succeeded" : "failed")} on target {target.name}.");
        return (success ? Result.SUCCESS : Result.FAILURE);
    }

    public PermaBuff() : base()
    {

    }

    public override BehaviorTree Copy()
    {
        return new PermaBuff();
    }
}
