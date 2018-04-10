using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	public GameObject spawnLocation;


	void OnTriggerEnter(Collider other) {
		Teleport(other.gameObject);
	}

	public void Teleport(GameObject obj) {
		obj.transform.position = spawnLocation.transform.position;
	}

}
