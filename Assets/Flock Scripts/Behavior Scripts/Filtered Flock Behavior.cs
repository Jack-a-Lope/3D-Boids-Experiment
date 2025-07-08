using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/SameFlock")]
public abstract class FilteredFlockBehavior : FlockBehavior
{
    public ContextFilter filter;
}
