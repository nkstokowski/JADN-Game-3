using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {

    public GameObject topPlayer;
    public GameObject bottomPlayer;
    public FlipScript flipper;

    public bool faceUp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void flipStatus(bool isTopShowing)
    {
        faceUp = isTopShowing;
        topPlayer.GetComponent<NavMeshAgent>().isStopped = true;
        bottomPlayer.GetComponent<NavMeshAgent>().isStopped = true;
    }
}
