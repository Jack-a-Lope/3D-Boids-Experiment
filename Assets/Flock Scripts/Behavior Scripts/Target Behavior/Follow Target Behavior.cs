using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/TargetFollow")]
public class TargetFollowingBehavior : Targeted
{
    
    Vector3 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, List<Transform> outerContext, GameObject target, Flock flock)
    {
        //if no neighbors then return no adjustment
        if (outerContext.Count == 0)
        {
            return Vector3.zero;
        }

        //add all of the points together and average them
        Vector3 followMove = Vector3.zero;

        List<Transform> filteredContext = (filter == null) ? outerContext : filter.Filter(agent, outerContext);

        foreach (Transform item in filteredContext)
        {
            if (item.GetComponent<Object>() == target.GetComponent<Object>())
            {
                followMove = (item.position - agent.transform.position);
                //Vector3 movementFinal = followMove * followMove.magnitude * followMove.magnitude;
                Debug.Log(followMove);
                return followMove;
            }
        }

        return Vector3.zero;
    }

}
