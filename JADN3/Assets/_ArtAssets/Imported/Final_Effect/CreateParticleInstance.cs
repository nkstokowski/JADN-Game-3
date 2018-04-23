using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateParticleInstance : MonoBehaviour {

    public GameObject system;
    
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(system, transform.position, transform.rotation);
        }
	}
}
