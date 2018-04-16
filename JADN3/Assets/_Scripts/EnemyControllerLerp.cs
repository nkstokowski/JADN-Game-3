using UnityEngine;
using System.Collections;


public class EnemyControllerLerp : MonoBehaviour
{

    public Transform[] points;
    public bool movingForward = true;
    public bool loopingPath = true;
    public float rotateSpeed = 2f;
    public float moveSpeed = 5f;
    private int destPoint = 0;


    void Start()
    {
        GotoNextPoint();
    }


    void Update()
    {
        Vector3 targetDir = points[destPoint].position - transform.position;

        // Handle Rotation
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

        // Rotate a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);

        // Handle Movement
        step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, points[destPoint].position, step);
        if(Vector3.Distance(transform.position, points[destPoint].position) < .5f)
        {
            GotoNextPoint();
        }
        transform.localPosition = new Vector3(transform.localPosition.x, .275f, transform.localPosition.z);
    }

    public void TurnAround()
    {
        movingForward = !movingForward;
        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Choose the next point in the array as the destination
        if (movingForward && loopingPath)
        {
            destPoint = (destPoint + 1) % points.Length;
        } else if(!movingForward && loopingPath)
        {
            destPoint = (destPoint == 0) ? points.Length - 1 : destPoint - 1;
        } else
        {
            if(movingForward && destPoint == (points.Length - 1))
            {
                movingForward = false;
            } else if(!movingForward && destPoint == 0)
            {
                movingForward = true;
            }

            destPoint += (movingForward) ? 1 : -1;
        }
    }
}