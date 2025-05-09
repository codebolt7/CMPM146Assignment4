using System.Collections.Generic;
using UnityEngine;

public class LowHPQuery : BehaviorTree
{
    float threshold;

    public LowHPQuery (float threshold) : base()
    {
        this.threshold = threshold;
    }

    public override Result Run()
    {
        float currentHealth = agent.hp.hp;
        return currentHealth <= threshold ? Result.SUCCESS : Result.FAILURE;
    }

    public override BehaviorTree Copy()
    {
        return new LowHPQuery(threshold);
    }

}
