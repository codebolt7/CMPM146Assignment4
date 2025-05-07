using System.Collections.Generic;

public class Selector : InteriorNode
{
    /// <summary>
    /// Will succeed as long as one of the children are free
    /// </summary>
    /// <returns></returns>
    public override Result Run()
    {
        // Check if in range
        if (current_child >= children.Count)
        {
            current_child = 0;
            return Result.FAILURE;
        }

        
        Result res = children[current_child].Run();
        
        // Restart on first success
        if (res == Result.SUCCESS)
        {
            current_child = 0;
            return Result.SUCCESS;
        }

        if (res == Result.FAILURE)
        {
            // Check if the last child
            if (current_child == children.Count - 1)
            {
                current_child = 0;
                return Result.FAILURE;
            }
            current_child++;
        }
        return Result.IN_PROGRESS;
    }

    public Selector(IEnumerable<BehaviorTree> children) : base(children)
    {
    }

    public override BehaviorTree Copy()
    {
        return new Selector(CopyChildren());
    }

}
