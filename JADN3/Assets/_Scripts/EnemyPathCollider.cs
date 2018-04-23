using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathCollider : MonoBehaviour {

    public EnemyControllerLerp parentEnemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        parentEnemy.TurnAround();
    }
}
