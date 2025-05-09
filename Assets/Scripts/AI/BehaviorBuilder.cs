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

                /*new Sequence(new BehaviorTree[] {
                    new FindTypeFollowTarget("skeleton",5f),
                    new FollowEnemy(5f),
                }),*/

                // launch global coordinated attack if threshold reached
                new Sequence(new BehaviorTree[]
                {
                    new ConditionQuery(),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),

                // check enemy count
                new Sequence(new BehaviorTree[]
                {
                    new NearbyEnemyThresholdQuery(10),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),

                // if too far from any target initially, head to a safe location
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
                    new NotNode(new WithinProximityToPlayer(agent.GetAction("attack").range)),
                    new GoTowards(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 2.5f, 1f)

                }),
                /*// prep for attack
                 new Sequence(new BehaviorTree[] {
                     new NearbyEnemiesQuery(1, 6f, "skeleton"),
                     new NearbyEnemiesQuery(3, 6f, "zombie"),
                     new MoveToPlayer(agent.GetAction("attack").range),
                     new Attack(),
                 }),*/
                // launch global coordinated attack if threshold reached
                new Sequence(new BehaviorTree[]
                {
                    new ConditionQuery(),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),

                // check enemy count
                new Sequence(new BehaviorTree[]
                {
                    new NearbyEnemyThresholdQuery(10),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),

                 // if too far from any target initially, head to a safe location
                new Sequence(new BehaviorTree[] {
                    new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 2.5f),
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
                    new NotNode(new WithinProximityToPlayer(agent.GetAction("attack").range)),
                    new GoTowards(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 2f, 1f)

                }),
                /*// prep for attack
                 new Sequence(new BehaviorTree[] {
                     new FindTypeFollowTarget("zombie", 6f),
                     new FollowEnemy(2f),
                     new Attack(),
                 }),*/
                // launch global coordinated attack if threshold reached
                new Sequence(new BehaviorTree[]
                {
                    new ConditionQuery(),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),

                // check enemy count
                new Sequence(new BehaviorTree[]
                {
                    new NearbyEnemyThresholdQuery(10),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),
                 // if too far from any target initially, head to a safe location
                new Sequence(new BehaviorTree[] {
                    new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 2.5f),
                })


            });
        }

        // do not change/remove: each node should be given a reference to the agent
        foreach (var n in result.AllNodes())
        {
            n.SetAgent(agent);
            n.AddBlackboard(blackboard);
            n.SetBBBool("coordAtk", false);
        }
        return result;
    }
}
