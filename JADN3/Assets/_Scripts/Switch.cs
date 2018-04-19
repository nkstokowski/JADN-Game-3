using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public enum SwitchAction {
        Move,
        Create
    }

public class Switch : MonoBehaviour, Interactable {

    public SwitchAction action;

    public bool turnedOn = false;

    private Vector3 aimPosition;

    public FlipScript flip;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }

    public Vector3 GetSpellHitPoint()
    {
        return transform.position;
    }

    public void OnSpellHit(Transform spell)
    {
        if(turnedOn == false)
        {
            turnedOn = true;
            print("turned on");
        }

        else if(turnedOn == true)
        {
            turnedOn = false;
            print("turned off");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Spell")
        {
            OnSpellHit(other.transform);
        }
    }
}
