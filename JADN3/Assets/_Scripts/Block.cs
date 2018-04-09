using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, Interactable {

    public float moveSpeed =1f;
    public float moveDistance = 1f;
    private bool moving;
    private Vector3 targetPosition;
    private enum MoveDir { Forward, Backward, Right, Left };

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, targetPosition) < .1f)
            {
                transform.position = targetPosition;
                //Debug.Log("Moving complete");
                moving = false;
            }
        }
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Spell" && !moving) {
            OnSpellHit(other.transform);
		} else if(other.tag == "Wall")
        {
            moving = false;
        }
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
        float movementCheckRange = moveDistance;

        // Set target position and ray range
        if (forward)
        {
            targetPosition = transform.position + (transform.forward * moveDistance);
            movementCheckRange += (transform.localScale.z / 2.0f);
        }else if (back)
        {
            targetPosition = transform.position + (-transform.forward * moveDistance);
            movementCheckRange += (transform.localScale.z / 2.0f);
        } else if (left)
        {
            targetPosition = transform.position + (-transform.right * moveDistance);
            movementCheckRange += (transform.localScale.x / 2.0f);
        } else if (right)
        {
            targetPosition = transform.position + (transform.right * moveDistance);
            movementCheckRange += (transform.localScale.x / 2.0f);
        }
        else
        {
            // Should hopefully never be reached
            Debug.Log("No movement direction found in Block.cs - OnSpellHit()");
        }

        moving = true;

        /* Check if the target position would collide with a wall
        if (PointValid(targetPosition, movementCheckRange))
        {
            Debug.Log("Movement Starting");
            moving = true;
        }*/
    }

    private bool PointValid(Vector3 point, float range)
    {
        RaycastHit hit;
        Vector3 direction = point - transform.position;
        return !Physics.Raycast(transform.position, direction, out hit, range);
    }
}
