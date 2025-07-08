using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/ObstacleAvoidance")]
public class ObstacleAvoidanceBehavior : FilteredFlockBehavior
{

    Vector3 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, List<Transform> outerContext, GameObject target, Flock flock)
    {
        //if no neighbors then return no adjustment
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        //add all of the points together and average them
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach (Transform item in filteredContext)
        {

            nAvoid++;
            avoidanceMove += (agent.transform.position - item.position);
                        
        }
        if (nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
            avoidanceMove = Vector3.SmoothDamp(agent.transform.forward, avoidanceMove, ref currentVelocity, agentSmoothTime);
        }

        return avoidanceMove;
    }

}
