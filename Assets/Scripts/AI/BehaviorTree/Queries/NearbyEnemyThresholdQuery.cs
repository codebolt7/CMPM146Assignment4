using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// checks count of all enemies on map
// i named it kinda bad sorrye
public class NearbyEnemyThresholdQuery : BehaviorTree
{
    int threshold;


    public override Result Run()
    {
        int count = 0;
        List<GameObject> enemies = GameManager.Instance.GetEnemiesInRange(Vector3.zero, float.MaxValue);
        foreach (var enemy in enemies)
        {
            string type = enemy.GetComponent<EnemyController>().monster;
            if (type == "zombie" || type == "skeleton")
            {
                count++;
            }
        }

        // if enough enemies on map, start the rush
        if (count >= threshold)
        {
            SetBBBool("coordAtk", true);
            Debug.Log("Coordinated Attack Launched");
            return Result.SUCCESS;
        }
        SetBBBool("coordAtk", false);
        return Result.FAILURE;
    }

    public NearbyEnemyThresholdQuery(int threshold) : base()
    {
        this.threshold = threshold;
    }

    public override BehaviorTree Copy()
    {
        return new NearbyEnemyThresholdQuery(threshold);
    }

}
