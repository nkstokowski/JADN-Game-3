using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Block : MonoBehaviour, Interactable {

    [Header("Size and Shape")]
    public int blockWidth = 1;
    public int blockLength = 1;
    public GameObject blockPrefab;
    public bool generateShape = false;

    [Header("Movement")]
    public float moveSpeed = 1f;
    public float moveDistance = 1f;

    private bool moving;
    private Vector3 targetPosition;
    private BoxCollider myCollider;
    private NavMeshObstacle myObstacle;
    private Vector3 aimPosition;
    private enum MoveDir { Forward, Backward, Right, Left };
	public FlipScript flip;

	// Use this for initialization
	void Start () {
		flip = GameObject.Find ("Levels").GetComponent<FlipScript> ();
        myCollider = GetComponent<BoxCollider>();
        myObstacle = GetComponent<NavMeshObstacle>();

        // Remake the block into the correct shape if asked
        if (generateShape)
        {
            GenerateShape();
        }

        // Setup the collider, navmesh obstacle, and aim position
        myCollider.size = new Vector3(blockLength, 1, blockWidth);
        myCollider.center = new Vector3(((float)blockLength / 2.0f) - 0.5f, 0, ((float)blockWidth / 2.0f) - 0.5f);
        myObstacle.size = myCollider.size;
        myObstacle.center = myCollider.center;
        aimPosition = myCollider.center;
    }
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
			flip.canFlip = false;
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, targetPosition) < .0001f)
            {
                transform.position = targetPosition;
                //Debug.Log("Moving complete");
                moving = false;
            }
        }
	}

    private void GenerateShape()
    {
        // Delete all child blocks
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Make and move the needed amount of blocks
        for(int i=0; i < blockLength; i++)
        {
            for(int j=0; j < blockWidth; j++)
            {
                GameObject block = Instantiate(blockPrefab, transform.position, transform.rotation, transform);
                block.transform.localPosition = new Vector3(i, 0, j);
            }
        }
    }

    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Spell" && !moving) {
            OnSpellHit(other.transform);
		} /*else if(other.tag == "Wall")
        {
            moving = false;
        }*/
	}

    public Vector3 GetSpellHitPoint()
    {
        return transform.TransformPoint(aimPosition);
    }

    public void OnSpellHit(Transform spell)
    {

        // Figure out what direction the block should move
        Vector3 pos = transform.InverseTransformPoint(spell.position);
        bool forward = Vector3.Dot(pos, Vector3.back) > 0.5f && Vector3.Dot(spell.forward, transform.forward) > 0.5f;
        bool back = Vector3.Dot(pos, Vector3.forward) > 0.5f && Vector3.Dot(spell.forward, -transform.forward) > 0.5f;
        bool left = Vector3.Dot(pos, Vector3.right) > 0.5f && Vector3.Dot(spell.forward, -transform.right) > 0.5f;
        bool right = Vector3.Dot(pos, Vector3.left) > 0.5f && Vector3.Dot(spell.forward, transform.right) > 0.5f;

        // Distance to shoot a ray to check for collisions
        Vector3 startPoint = transform.position;
        Vector3 endPoint;
        float movementCheckRange = moveDistance;
        bool valid;

        // Set target position and ray range
        if (forward)
        {
            startPoint.z += (blockWidth - 1);
            endPoint = startPoint + (transform.forward * moveDistance);
            targetPosition = transform.position + (transform.forward * moveDistance);
            moving = MoveValid(startPoint, endPoint, blockLength, 1, 0);
            //Debug.Log("Forward");
        }else if (back)
        {
            endPoint = startPoint + (-transform.forward * moveDistance);
            targetPosition = transform.position + (-transform.forward * moveDistance);
            moving = MoveValid(startPoint, endPoint, blockLength, 1, 0);
            //Debug.Log("Back");
        } else if (left)
        {
            endPoint = startPoint + (-transform.right * moveDistance);
            targetPosition = transform.position + (-transform.right * moveDistance);
            moving = MoveValid(startPoint, endPoint, blockWidth, 0, 1);
            //Debug.Log("Left");
        } else if (right)
        {
            startPoint.x += (blockLength - 1);
            endPoint = startPoint + (transform.right * moveDistance);
            targetPosition = transform.position + (transform.right * moveDistance);
            moving = MoveValid(startPoint, endPoint, blockWidth, 0, 1);
            //Debug.Log("Right");
        }
        else
        {
            // Should hopefully never be reached
            // Turns out it does get reached occasionally
            // If someone reading this has the time and knows more about
            // Vector math than I do, check out the four bools above and
            // figure out if anything can be done
            Debug.Log("No movement direction found in Block.cs - OnSpellHit()");
        }

        //moving = true;
        //Debug.Log(moving);
        //Debug.Log("Starting point: " + startPoint + ", target point: " + targetPosition);
       
    }


    private bool MoveValid(Vector3 start, Vector3 target, int limit, float xinc, float zinc)
    {
        Vector3 pointA = start;
        Vector3 pointB = target;
        for(int i=0; i < limit; i++)
        {
            //Debug.Log("Drawing Ray from: " + pointA + ", to " + pointB);
            if(!PointValid(pointA, pointB, moveDistance))
            {
                return false;
            }
            pointA.x += xinc;
            pointA.z += zinc;
            pointB.x += xinc;
            pointB.z += zinc;
        }
        return true;
    }

    private bool PointValid(Vector3 start, Vector3 end, float range)
    {
        RaycastHit hit;
        Vector3 direction = end - start;
        return !Physics.Raycast(start, direction, out hit, range);
    }
}
