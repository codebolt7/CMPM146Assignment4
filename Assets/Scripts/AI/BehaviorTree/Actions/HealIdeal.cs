using UnityEngine;

public class HealIdeal : BehaviorTree
{
    public override Result Run()
    {
        var target = GetBBEnemy("IdealHealee");
        if (target == null) return Result.FAILURE;

        EnemyAction act = agent.GetAction("heal");
        if (act == null) return Result.FAILURE;

        bool success = act.Do(target.transform);
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
