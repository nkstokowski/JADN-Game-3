using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name == "Spell") {
			//Destroy (this.gameObject);
			//this.transform.Translate (other.transform.forward * 3f * Time.deltaTime);
			//this.gameObject.GetComponent<Rigidbody> ().AddForce (other.transform.forward * Time.deltaTime);
			this.transform.Translate(other.transform.forward * 20f * Time.deltaTime);
		}
	}
}
