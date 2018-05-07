using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    [Header("Dependencies")]
    public Camera cam;
    public NavMeshAgent agent;
    ObjectPooling objectPooler;
    public Animator playerAnimator;

    [Header("Rotation")]
    public float rotationSpeed = 20f;

    public float speed;

    private bool rotating = false;
    private Quaternion targetRotation;
    private Vector3 lastPosition;

    void Start()
    {
        objectPooler = ObjectPooling.Instance;
        lastPosition = transform.position;
    }

    void Update () {

        if (Input.GetMouseButtonDown(0) && cam.gameObject.activeSelf)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                switch (hit.transform.tag)
                {
                    case "Interact":
                        TurnTowards(hit.transform);
                        break;
				default:
                        if (agent.enabled)
                            MoveTo (hit.point);
                        break;
                }


            }
        }

        // Rotation is active
        if (rotating)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, targetRotation) < 5f)
            {
                // Complete Rotation
                //Debug.Log("Rotation Done");
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
                transform.rotation = targetRotation;
                rotating = false;

                // Fire spell
                //Debug.Log("Firing Spell");
                objectPooler.SpawnFromPool(transform.position, transform.rotation, "Spell");
            }
        }
    }

    void FixedUpdate()
    {
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
        lastPosition = transform.position;
        playerAnimator.SetFloat("Speed", speed);
    }

    // Fire a spell at transform
    private void TurnTowards(Transform target)
    {
        //Debug.Log("Starting Rotation");
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        // Rotate to transform
        Interactable obj = target.GetComponent<Interactable>();
        Vector3 targetAimPoint = obj.GetSpellHitPoint();
        targetRotation = Quaternion.LookRotation(targetAimPoint - transform.position);
        rotating = true;

        float angle = Vector3.Angle(transform.position, ((Component)obj).transform.position);
        playerAnimator.SetTrigger("Cast");
        Debug.Log(angle);
        playerAnimator.SetFloat("Angle", angle);

        // Spell fires once rotation is complete
    }

    // Move to a point
    private void MoveTo(Vector3 point)
    {
        agent.isStopped = false;
        agent.SetDestination(point);

    }
}
