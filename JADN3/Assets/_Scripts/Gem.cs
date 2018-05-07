using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	public float rotateSpeed = 75.0f;
	public float floatSpeed = 3.0f;
	public string loadSceneName;
	public GameObject manager;
	private bool isFinal = false;
	private bool startFloat = false;
	public Vector3 finalPosition;

	void Update(){
		transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

		if(isFinal) {
			manager.GetComponent<GameManager>().LoadLevel(loadSceneName);
		} else if(startFloat) {
			GemFloat();
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player") {
			startFloat = true;
			GemFloat();
		}
	}

	void GemFloat(){
		transform.localPosition = Vector3.Lerp(transform.localPosition,finalPosition,Time.deltaTime * floatSpeed);
		Color tempColor = GetComponent<MeshRenderer>().material.color;
		tempColor.a -= (Time.deltaTime * floatSpeed);
		GetComponent<MeshRenderer>().material.color = tempColor;
		if(Vector3.Distance(transform.localPosition,finalPosition) <= 20f) {
			startFloat = false;
			isFinal = true;
		}
	}

}
