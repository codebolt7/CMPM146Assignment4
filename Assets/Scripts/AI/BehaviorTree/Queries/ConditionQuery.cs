using UnityEngine;
// also kinda named this bad

public class ConditionQuery : BehaviorTree
{
    public ConditionQuery() : base() { }

    public override Result Run()
    {
        bool condition = GetBBBool("coordAtk");
        return condition ? Result.SUCCESS : Result.FAILURE;
    }

    public override BehaviorTree Copy()
    {
        return new ConditionQuery();
    }

}
