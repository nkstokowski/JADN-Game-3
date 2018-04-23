using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Block : MonoBehaviour, Interactable {

    public enum BlockLayer { Top = 10, Bottom = 11 };
    [Header("Layer")]
    public BlockLayer currentLayer = BlockLayer.Top;

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
    private Vector3 originalPosition;
    private Vector3 hitDirection;
    private BoxCollider myCollider;
    private NavMeshObstacle myObstacle;
    private Vector3 aimPosition;

	// Use this for initialization
	void Start () {

        myCollider = GetComponent<BoxCollider>();
        myObstacle = GetComponent<NavMeshObstacle>();
        originalPosition = transform.localPosition;

        // Remake the block into the correct shape if asked
        if (generateShape)
        {
            GenerateShape();
        }
        else
        {
            SetBlockLayer((int)currentLayer);
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
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, targetPosition) < .01f)
            {
                transform.position = targetPosition;
                //Debug.Log("Moving complete");
                moving = false;
                originalPosition = transform.localPosition;
            }
        }
	}

    public void SetBlockLayer(int layerSet)
    {
        // Set object and children to new layer
        foreach (Transform t in transform)
        {
            t.gameObject.layer = layerSet;
        }

        // Update currentlayer var
        currentLayer = (BlockLayer)layerSet;
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
                block.layer = (int) currentLayer;
            }
        }
    }

    public void SetHitDirection(Transform spell){
        // Figure out what direction the block should move
        Vector3 pos = transform.InverseTransformPoint(spell.position);
        bool forward = Vector3.Dot(pos, Vector3.back) > 0.5f && Vector3.Dot(spell.forward, transform.forward) > 0.5f;
        bool back = Vector3.Dot(pos, Vector3.forward) > 0.5f && Vector3.Dot(spell.forward, -transform.forward) > 0.5f;
        bool left = Vector3.Dot(pos, Vector3.right) > 0.5f && Vector3.Dot(spell.forward, -transform.right) > 0.5f;
        bool right = Vector3.Dot(pos, Vector3.left) > 0.5f && Vector3.Dot(spell.forward, transform.right) > 0.5f;
        if (forward)
            hitDirection = transform.forward;
        else if (back)
            hitDirection = -transform.forward;
        else if (left)
            hitDirection = -transform.right;
        else if (right)
            hitDirection = transform.right;
    }

    public Vector3 GetSpellHitPoint()
    {
        return transform.TransformPoint(aimPosition);
    }

    public void OnSpellHit(Transform spell)
    {
        if (moving)
        {
            targetPosition += hitDirection;
        }
        SetHitDirection(spell);
        // Distance to shoot a ray to check for collisions
        Vector3 startPoint = transform.position;
        Vector3 endPoint;

        // Set target position and ray range
        if (hitDirection == transform.forward)
        {
            startPoint.z += (blockWidth - 1);
            endPoint = startPoint + (transform.forward * moveDistance);
            targetPosition = transform.position + (transform.forward * moveDistance);
            moving = MoveValid(startPoint, endPoint, blockLength, 1, 0);
            //Debug.Log("Forward");
        }else if (hitDirection == -transform.forward)
        {
            endPoint = startPoint + (-transform.forward * moveDistance);
            targetPosition = transform.position + (-transform.forward * moveDistance);
            moving = MoveValid(startPoint, endPoint, blockLength, 1, 0);
            //Debug.Log("Back");
        } else if (hitDirection == -transform.right)
        {
            endPoint = startPoint + (-transform.right * moveDistance);
            targetPosition = transform.position + (-transform.right * moveDistance);
            moving = MoveValid(startPoint, endPoint, blockWidth, 0, 1);
            //Debug.Log("Left");
        } else if (hitDirection == transform.right)
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
        if (Physics.Raycast(start, direction, out hit, range))
        {
            return (hit.transform.tag == "Portal" || hit.transform.tag == "FloorSwitch");
        }
        else
        {
            return true;
        }
    }

    public void ResetPosition()
    {
        moving = false;
        transform.localPosition = originalPosition;
    }

    public void SetTargetPosition(Vector3 newPosition){
        targetPosition = newPosition;
    }

    public void TeleportBlock(Vector3 pos, bool swapLayer)
    {
        targetPosition = pos;
        originalPosition = pos;
        if (swapLayer)
        {
            SwapBlockLayer();
        }
    }

    public void SwapBlockLayer()
    {
        // Get the opposite of the current layer, as an int
        int layerSet = (currentLayer == BlockLayer.Top) ? (int)BlockLayer.Bottom : (int)BlockLayer.Top;

        // Set object and children to new layer
        foreach (Transform t in GetComponentInChildren<Transform>())
        {
            t.gameObject.layer = layerSet;
        }

        // Update currentlayer var
        currentLayer = (BlockLayer)layerSet;

    }

}
