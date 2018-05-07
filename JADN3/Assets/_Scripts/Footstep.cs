using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour {

	public void Step() {
		Debug.Log("step");
		FindObjectOfType<AudioManager>().PlaySoundWithTag("Step");
	}
}
