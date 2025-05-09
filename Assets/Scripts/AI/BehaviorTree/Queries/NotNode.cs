using System.Collections.Generic;
public class NotNode : BehaviorTree
{
    private BehaviorTree child;

    public NotNode(BehaviorTree child) : base()
    {
        this.child = child;
    }

    public override BehaviorTree Copy()
    {
        return new NotNode(child.Copy());
    }

    public override Result Run()
    {
        Result res = child.Run();
        if (res == Result.SUCCESS)
        {
            return Result.FAILURE;
        }
        else if (res == Result.FAILURE)
        {
            return Result.SUCCESS;
        }
        return res;
    }

    public override IEnumerable<BehaviorTree> AllNodes()
    {
        yield return this;
        foreach (var n in child.AllNodes())
        {
            yield return n;
        }
    }

}
