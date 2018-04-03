using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    public Camera cam;
    public NavMeshAgent agent;


    void Update () {
        if (Input.GetMouseButtonDown(0) && cam.gameObject.activeSelf)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                agent.isStopped = false;
                agent.SetDestination(hit.point);
            }
        }
    }
}
