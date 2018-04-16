﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Portal : MonoBehaviour {

	public GameObject spawnLocation;
	public bool canSendPlayer = false;


	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player" && !canSendPlayer) return;

		if(other.gameObject.tag == "Player" && canSendPlayer) {
			other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
		}

		Teleport(other.gameObject);
	}

	public void Teleport(GameObject obj) {
		obj.transform.position = spawnLocation.transform.position;
		obj.layer = spawnLocation.layer;

		if(obj.tag == "Player"){
			obj.GetComponent<NavMeshAgent>().enabled = true;
		}
	}

}