using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 250;
    const float agentDensity = 0.2f;

    //This is a scaler to modify the movement of the boids
    [Range(0f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 1000f)]
    public float neighborRadius = 20f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;
    [Range(1f, 1000f)]
    public float obstacleAvoidanceRadius = 50f;
    [Range(1f, 1000f)]
    public float targetRadius = 150f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    GameObject target;

    void Start()
    {
        target = GameObject.FindWithTag("Target");
        Debug.Log(target);
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitSphere * startingCount * agentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }
    private void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            List<Transform> outerContext = GetFarawayObjects(agent);


            //Shows the number of neighbors based off of color; FOR DEMO ONLY
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            Vector3 move = behavior.CalculateMove(agent, context, outerContext, target, this);
            move *= driveFactor;
            if (move.sqrMagnitude > maxSpeed)
            {
                move = move.normalized * maxSpeed;
            }

            if (move != Vector3.zero)
            {
                agent.Move(move);
            }
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();

        //for 3d switch to 3D colliders and physics3D.OverlapSphereAll
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }

    List<Transform> GetFarawayObjects(FlockAgent agent)
    {
        List<Transform> outerContext = new List<Transform>();

        //for 3d switch to 3D colliders and physics3D.OverlapSphereAll
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach (Collider c in contextColliders)
        {
            if (c.GetComponent<FlockAgent>() == null)
            {
                outerContext.Add(c.transform);
            }
        }

        return outerContext;
    }
}
