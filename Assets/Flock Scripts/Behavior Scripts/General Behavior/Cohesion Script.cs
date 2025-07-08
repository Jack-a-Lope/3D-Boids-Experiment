using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohession")]
public class CohessionBehavior : FilteredFlockBehavior
{

    Vector3 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public float visionRadius = 240;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, List<Transform> outerContext, GameObject target, Flock flock)
    {
        //if no neighbors then return no adjustment
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        //add all of the points together and average them
        Vector3 cohessionMove = Vector3.zero;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach (Transform item in filteredContext)
        {
            float angle = Vector3.Angle(agent.transform.forward, item.forward);
            if (angle <= visionRadius)
            {
                cohessionMove += item.position;
            }
        }
        cohessionMove /= context.Count;

        //create offset from agent position
        cohessionMove -= agent.transform.position;
        cohessionMove = Vector3.SmoothDamp(agent.transform.forward, cohessionMove, ref currentVelocity, agentSmoothTime);


        return cohessionMove;
    }

}
