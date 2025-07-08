using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/TargetFlock")]
public abstract class Targeted : FlockBehavior
{
    public ContextFilter filter;
}
