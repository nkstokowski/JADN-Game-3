using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch : Switch, Interactable {

    private Vector3 aimPosition;

    // Use this for initialization
    void Start()
    {
        BuildTargetList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 GetSpellHitPoint()
    {
        return transform.position;
    }

    public void OnSpellHit(Transform spell)
    {
        switchOn = !switchOn;
        if(switchType == SwitchType.Switch)
        {
            if (switchOn)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
        }
        else
        {
            Trigger();
        }

    }
}
