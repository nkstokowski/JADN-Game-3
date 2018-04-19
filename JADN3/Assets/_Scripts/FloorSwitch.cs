﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour {

    public SwitchAction action;
    public bool triggered = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.name != "Ground")
        {
            triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name != "Ground")
        {
            triggered = false;
        }
    }
}
