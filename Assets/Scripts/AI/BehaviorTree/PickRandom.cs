using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class PickRandom : InteriorNode 
{
    public override Result Run()
    {
        if (children.Count == 0) return Result.FAILURE;

        if (current_child < 0)
        {
            current_child = Random.Range(0, children.Count);
        }

        Result res = children[current_child].Run();

        if (res != Result.IN_PROGRESS)
        {
            // Reroll if finished
            current_child = -1;
        }

        return res;
    }

    public PickRandom(IEnumerable<BehaviorTree> children) : base (children)
    {
    }

    public override BehaviorTree Copy()
    {
        return new PickRandom(CopyChildren());
    }
}
