using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Portal : MonoBehaviour, SwitchTarget {

	public GameObject spawnLocation;
	public bool canSendPlayer = false;
    public bool sendsAcrossLayer = true;
    public bool portalActive = true;
    public bool canTurnOff = false;

    void OnTriggerEnter(Collider other) {
        if (!portalActive) return;

		if(other.gameObject.tag == "Player" && !canSendPlayer) return;
        else if(other.gameObject.tag != "Player" && canSendPlayer) return;
        else{
            if(other.gameObject.tag == "Player"){
                other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
            Teleport(other.gameObject);
        }	
	}

    public void Teleport(GameObject obj)
    {
        obj.transform.position = spawnLocation.transform.position;
        //obj.layer = spawnLocation.layer;
        FindObjectOfType<AudioManager>().PlaySoundWithTag("Teleport");

        if (obj.tag == "Player")
        {
            obj.GetComponent<NavMeshAgent>().enabled = true;
        }
        else if (obj.tag == "Interact")
        {
            Block blockScript = obj.GetComponent<Block>();
            if (blockScript)
            {
                blockScript.TeleportBlock(obj.transform.position, sendsAcrossLayer);
            }
        }
    }

    public void HandleSwitchOn()
    {
        FindObjectOfType<AudioManager>().PlaySoundWithTag("PortalOn");
        portalActive = true;
		gameObject.GetComponent<BoxCollider> ().enabled = true;
		transform.Find ("Portal").gameObject.SetActive (true);
		transform.Find ("Inner").gameObject.SetActive (true);
		transform.Find ("Outer").gameObject.SetActive (true);
    }

    public void HandleSwitchOff()
    {
        if (canTurnOff)
        {
			transform.Find ("Portal").gameObject.SetActive (false);
            portalActive = false;
        }
    }

    public void HandleSwitchTrigger()
    {

    }

}