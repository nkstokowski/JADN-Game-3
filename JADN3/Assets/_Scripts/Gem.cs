using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	public float rotateSpeed = 75.0f;
	void Update(){
		transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
	}
}
