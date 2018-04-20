﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{

    public Transform[] points;
    public Transform[] pointsTest;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public bool getPointsFromName = false;
    public string pointsObjectName;
    public int startingPointIndex = 0;


    void Start()
    {

        if (getPointsFromName && pointsObjectName != "")
        {
            getPointsFromObject();
        }

        agent = GetComponent<NavMeshAgent>();
        if(startingPointIndex < points.Length)
        {
            destPoint = startingPointIndex;
        }

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as ita
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void getPointsFromObject()
    {
        GameObject pointsObject = GameObject.Find(pointsObjectName);

        if (pointsObject)
        {
            List<Transform> allTransforms = new List<Transform>();
            foreach(Transform t in pointsObject.GetComponentsInChildren<Transform>())
            {
                allTransforms.Add(t);
            }
            allTransforms.RemoveAt(0);
            points = allTransforms.ToArray();
        }
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.enabled && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }
}