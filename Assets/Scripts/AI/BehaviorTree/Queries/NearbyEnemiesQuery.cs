public class NearbyEnemiesQuery : BehaviorTree
{
    int count;
    float distance;
    string requiredType; // updated, now can check for count of a specific type of nearby enemy optionally

    public override Result Run()
    {
        var nearby = GameManager.Instance.GetEnemiesInRange(agent.transform.position, distance);
        if (nearby.Count >= count)
        {
            return Result.SUCCESS;
        }

        if (requiredType != null)
        {
            int typeCount = 0;
            foreach (var enemy in nearby)
            {
                if (enemy == this.agent.gameObject) continue; 

                if (enemy.GetComponent<EnemyController>().monster == requiredType)
                {
                    typeCount++;

                }
            }

            if (typeCount >= count)
            {
                return Result.SUCCESS;
            }
        }

        return Result.FAILURE;
    }

    public NearbyEnemiesQuery(int count, float distance, string requiredType = null) : base()
    {
        this.count = count;
        this.distance = distance;
        this.requiredType = requiredType;
    }

    public override BehaviorTree Copy()
    {
        return new NearbyEnemiesQuery(count, distance);
    }
}
