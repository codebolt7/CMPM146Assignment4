using UnityEngine;
using System.Collections.Generic;   // need to add this to other scripts to use Dictionary

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        BehaviorTree result = null;
        Dictionary<string, object> blackboard = new Dictionary<string, object>();

        if (agent.monster == "warlock")
        {
            result = new Selector(new BehaviorTree[]
            {
                // heal ideal ally if ability ready and target found
                new Sequence(new BehaviorTree[] {
                    new AbilityReadyQuery("heal"),
                    new FindIdealHealee(5f, 10),
                    new HealIdeal()
                }),
                // perma buff strongest nearby enemy if available
                new Sequence(new BehaviorTree[] {
                    new AbilityReadyQuery("permabuff"),
                    new StrengthFactorQuery(1.5f),
                    new PermaBuff()
                }),

                // temp buff a decent target
                new Sequence(new BehaviorTree[] {
                    new AbilityReadyQuery("buff"),
                    new StrengthFactorQuery(1.0f),
                    new Buff()
                }),

                // otherwise stay back
                // edit to make them follow skeletons at a distance? and change from a GoTo
                new Sequence(new BehaviorTree[] {
                    new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 1f),
                })

            });
        }
        else if (agent.monster == "zombie")
        {

            // might swap out GoTo for GoTowards idk yet
            result = new Selector(new BehaviorTree[]
            {
                // if low hp, retreat to get healed
                new Sequence(new BehaviorTree[]
                {
                    new LowHPQuery(25),
                    new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 2.5f)

                }),
                // else prep for attack
                 new Sequence(new BehaviorTree[] {
                     new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 2.5f),
                     new NearbyEnemiesQuery(2, 15f, "skeleton" ),
                     new NearbyEnemiesQuery(5, 15f, "zombie"),                // wait for more zombies 
                     new MoveToPlayer(agent.GetAction("attack").range),
                     new Attack(),
                 })

            });
        }
        else // skeletons
        {
            result = new Selector(new BehaviorTree[]
            {
                // if low hp, retreat to get healed
                new Sequence(new BehaviorTree[]
                {
                    new LowHPQuery(25),
                    new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 2f)

                }),
                // else prep for attack
                 new Sequence(new BehaviorTree[] {
                     new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 2f),
                     new NearbyEnemiesQuery(5, 15f, "zombie"),                // wait for zombies
                     new NotNode(new NearbyEnemiesQuery(2, 15f, "zombie")),   // when zombies leave, follow -> not working as expected
                     new MoveToPlayer(agent.GetAction("attack").range),
                     new Attack(),
                 })

            });
        }

        // do not change/remove: each node should be given a reference to the agent
        foreach (var n in result.AllNodes())
        {
            n.SetAgent(agent);
            n.AddBlackboard(blackboard);
        }
        return result;
    }
}
