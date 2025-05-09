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
            /* result = new Sequence(new BehaviorTree[] {
                                        new MoveToPlayer(agent.GetAction("attack").range),
                                        new Attack(),
                                        new PermaBuff(),
                                        new Heal(),
                                        new Buff()
                                     });
            */
            result = new Selector(new BehaviorTree[]
            {
                // heal ideal ally if ability ready and target found
                new Sequence(new BehaviorTree[] {
                    new AbilityReadyQuery("heal"),
                    new FindIdealHealee(5f, 15f),
                    new HealIdeal()
                }),
                // perma buff strongest nearby enemy if available
                new Sequence(new BehaviorTree[] {
                    new AbilityReadyQuery("permabuff"),
                    new StrengthFactorQuery(2.0f),
                    new PermaBuff()
                }),

                // temp buff a decent target
                new Sequence(new BehaviorTree[] {
                    new AbilityReadyQuery("buff"),
                    new StrengthFactorQuery(1.5f),
                    new Buff()
                }),

                // otherwise stay back
                // edit to make them follow skeletons at a distance? and change from a GoTo
                new Sequence(new BehaviorTree[] {
                    new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 3f),
                })

            });
        }
        else if (agent.monster == "zombie")
        {
            /* result = new Sequence(new BehaviorTree[] {
                                       new MoveToPlayer(agent.GetAction("attack").range),
                                       new Attack()
                                     });
            */

            // might swap out GoTo for GoTowards idk yet
            result = new Selector(new BehaviorTree[]
            {
                // if low hp, retreat to get healed
                new Sequence(new BehaviorTree[]
                {
                    new LowHPQuery(25),
                    new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 3f)

                }),
                // else prep for attack
                 new Sequence(new BehaviorTree[] {
                     new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 3f),
                     new NearbyEnemiesQuery(5, 15f, "zombie"),                // wait for more zombies
                     new MoveToPlayer(agent.GetAction("attack").range),
                     new Attack()
                 })

            });
        }
        else // skeletons
        {
            /*result = new Sequence(new BehaviorTree[] {
                                       new MoveToPlayer(agent.GetAction("attack").range),
                                       new Attack()
                                     });
            */
            result = new Selector(new BehaviorTree[]
            {
                // if low hp, retreat to get healed
                new Sequence(new BehaviorTree[]
                {
                    new LowHPQuery(25),
                    new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 3f)

                }),
                // else prep for attack
                 new Sequence(new BehaviorTree[] {
                     new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 3f),
                     new NearbyEnemiesQuery(5, 15f, "zombie"),                // wait for zombies
                     new NotNode(new NearbyEnemiesQuery(5, 15f, "zombie")),   // when zombies leave, follow -> might need to slow them further?
                     new MoveToPlayer(agent.GetAction("attack").range),
                     new Attack()
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
