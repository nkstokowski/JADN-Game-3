using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indication : MonoBehaviour {

	public GameObject flipObject;

	public void Flip(){
		flipObject.GetComponent<FlipScript>().TriggerFlip();
	}

	public void TriggerNotification(){

	}
}
