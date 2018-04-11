using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Switch : MonoBehaviour, Interactable {

    public bool turnedOn = false;

    private BoxCollider switchCollider;
    private Vector3 aimPosition;

    public FlipScript flip;

    // Use this for initialization
    void Start () {
        flip = GameObject.Find("Levels").GetComponent<FlipScript>();
        switchCollider = GetComponent<BoxCollider>();

        aimPosition = switchCollider.center;
	}
	
	// Update is called once per frame
	void Update () {

    }

    public Vector3 GetSpellHitPoint()
    {
        return transform.TransformPoint(aimPosition);
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

    void onTriggerEnter(Collider other)
    {
        if(other.tag == "Spell")
        {
            OnSpellHit(other.transform);
        }
    }
}
