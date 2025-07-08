using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StayInRadius")]
public class StayInRadius : FlockBehavior
{
    Vector3 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public Vector3 center;
    public float radius;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, List<Transform> outerContext, GameObject target, Flock flock)
    {
        Vector3 centerOffset = center - agent.transform.position;
        float t = centerOffset.magnitude / radius;

        if (t < 0.9f)
        {
            return Vector3.zero;
        }

        else
        {
            //centerOffset = Vector3.SmoothDamp(agent.transform.forward, centerOffset, ref currentVelocity, agentSmoothTime);
            return centerOffset * t * t;
        }
    }


}
