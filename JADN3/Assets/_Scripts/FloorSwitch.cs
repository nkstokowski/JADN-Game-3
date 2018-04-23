using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : Switch {

    void Start()
    {
        BuildTargetList();
    }

    // Update is called once per frame
    void Update () {

	}

    // Turn switch on when something enters 
    private void OnTriggerEnter(Collider other)
    {
        if(other.name != "Ground")
        {
            switchOn = true;

            TurnOn();

        }
    }

    // Turn switch off when something leaves
    private void OnTriggerExit(Collider other)
    {
        if (other.name != "Ground")
        {
            switchOn = false;

            TurnOff();

        }
    }
}
