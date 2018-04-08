using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

	public float speed = 5.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MoveObject ();
	}

	void MoveObject()
	{
		this.transform.Translate (0, 0, speed * Time.deltaTime);
	}
	//If this hits something, best to destory this object
	void OnTriggerEnter(Collider other)
	{
		Destroy (this.gameObject);
	}
}
