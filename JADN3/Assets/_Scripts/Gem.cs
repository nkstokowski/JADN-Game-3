using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	public float rotateSpeed = 75.0f;
	public string loadSceneName;
	public GameObject manager;
	void Update(){
		transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player") {
			manager.GetComponent<GameManager>().LoadLevel(loadSceneName);
		}
	}

}
