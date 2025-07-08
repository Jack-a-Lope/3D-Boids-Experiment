using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{

    Vector3 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public float visionRadius = 240;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, List<Transform> outerContext, GameObject target, Flock flock)
    {
        //if no neighbors then maintain current allignment
        if (context.Count == 0)
        {
            return agent.transform.forward;
        }

        //add all of the points together and average them
        Vector3 alignmentMove = Vector3.zero;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach (Transform item in filteredContext)
        {
            float angle = Vector3.Angle(agent.transform.forward, item.forward);
            if (angle <= visionRadius)
            {
                alignmentMove += item.transform.forward;
            }

        }
        alignmentMove /= context.Count;
        //alignmentMove = Vector3.SmoothDamp(agent.transform.forward, alignmentMove, ref currentVelocity, agentSmoothTime);


        return alignmentMove;
    }

}
