using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

	public float speed = 5.0f;

    ObjectPooling objectPooler;


    // Use this for initialization
    void Start () {
        objectPooler = ObjectPooling.Instance;
    }
	
	// Update is called once per frame
	void Update () {
		MoveObject ();
	}

    void MoveObject()
    {
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }

	//If this hits something, add it back to the object pool
	void OnTriggerEnter(Collider other)
	{
        objectPooler.ReQueue(this.gameObject, "Spell");

    }
}
