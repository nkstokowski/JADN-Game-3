﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indication : MonoBehaviour {

	public GameObject flipObject;
	public GameObject flipButton;
	public AnimationClip flashAnimation;
	private bool isFlashing = false;

	public void Flip(){
		flipObject.GetComponent<FlipScript>().TriggerFlip();
		Color temp = flipButton.GetComponent<Image>().color;
		temp.a = 255f;
		flipButton.GetComponent<Image>().color = temp;
		isFlashing = false;
	}

	void Update(){
		if(isFlashing)
			flipButton.GetComponent<Animation>().Play("Notification");
	}

	public void TriggerNotification(){
		isFlashing = !isFlashing;
	}
}
