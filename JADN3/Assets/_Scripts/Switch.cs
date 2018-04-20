using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public enum SwitchAction {
        Move,
        Create,
		Portal
    }

public class Switch : MonoBehaviour, Interactable {

    public SwitchAction action;

    public bool triggered = false;

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
		if(triggered == false)
        {
			triggered = true;
            print("turned on");
        }

		else if(triggered == true)
        {
			triggered = false;
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
