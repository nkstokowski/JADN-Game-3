using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyaerShoot : MonoBehaviour {

	public GameObject spell;
	public Transform shoot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		castSpell ();
	}
	void castSpell() {
		if(Input.GetKeyDown(KeyCode.W))
			{
				GameObject obj = Instantiate (spell, shoot.position, shoot.rotation) as GameObject;
				obj.name = "Spell";
			}
	}
}
