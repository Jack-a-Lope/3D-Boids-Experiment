using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }
    CapsuleCollider agentCollider;
    public CapsuleCollider AgentCollider { get { return agentCollider; } }

    private void Start()
    {
        agentCollider = GetComponent<CapsuleCollider>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector3 velocity)
    {
        //Transform.forward for 3D
        //Transform is a vector 3 which is why when adding vector 2 velocity it is necessary to cast to vector 3
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;


    }
}
