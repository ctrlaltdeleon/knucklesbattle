using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour
{

    private int flockSize = 20;
    public float SpeedModifier = 5;

    //SerializeField allows for fixing within the inspector, could just change to public
    [SerializeField]
    private float alignmentWeight = 1;

    [SerializeField]
    private float cohesionWeight = 1;

    [SerializeField]
    private float separationWeight = 1;

    [SerializeField]
    private float followWeight = 5;

    [SerializeField]
    private ExtraMrMeeseeksAI prefab;

    [SerializeField]
    private float spawnRadius = 3.0f;
    private Vector3 spawnLocation = Vector3.zero;

    [SerializeField]
    public Transform target;

    List<ExtraMrMeeseeksAI> flockList = new List<ExtraMrMeeseeksAI>(3);

    void Awake()
    {
        for (int i = 0; i < flockSize; i++)
        {
            spawnLocation = Random.insideUnitSphere * spawnRadius + transform.position;
            ExtraMrMeeseeksAI boid = Instantiate(prefab, spawnLocation, transform.rotation) as ExtraMrMeeseeksAI;

            boid.transform.parent = transform;
            boid.FlockController = this;
            flockList.Add(boid);
        }
    }

    public Vector3 Flock(ExtraMrMeeseeksAI boid, Vector3 boidPosition, Vector3 boidDirection)
    {
        Vector3 flockDirection = Vector3.zero;
        Vector3 flockCenter = Vector3.zero;
        Vector3 targetDirection = Vector3.zero;
        Vector3 separation = Vector3.zero;

        for (int i = 0; i < flockList.Count; ++i)
        {
            ExtraMrMeeseeksAI neighbor = flockList[i];
            //Check only against neighbors.
            if (neighbor != boid)
            {
                //Aggregate the direction of all the boids.
                flockDirection += neighbor.Direction;
                //Aggregate the position of all the boids.
                flockCenter += neighbor.transform.localPosition;
                //Aggregate the delta to all the boids.
                separation += neighbor.transform.localPosition - boidPosition;
                separation *= -1;
            }
        }
        //Alignment. The average direction of all boids.
        flockDirection /= flockSize;
        flockDirection = flockDirection.normalized * alignmentWeight;

        //Cohesion. The centroid of the flock.
        flockCenter /= flockSize;
        flockCenter = flockCenter.normalized * cohesionWeight;

        //Separation.
        separation /= flockSize;
        separation = separation.normalized * separationWeight;

        //Direction vector to the target of the flock.
        targetDirection = target.localPosition - boidPosition;
        targetDirection = targetDirection * followWeight;

        return flockDirection + flockCenter + separation + targetDirection;
    }
}
