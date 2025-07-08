using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/AgentFilter")]
public class AgentFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();

        foreach (Transform item in original)
        {
            
            if (item.GetComponent<FlockAgent>() != null)
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
