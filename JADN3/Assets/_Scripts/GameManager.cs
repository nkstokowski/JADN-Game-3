using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {

    public GameObject topPlayer;
    public GameObject bottomPlayer;
    public FlipScript flipper;

    ObjectPooling objectPooler;

    public bool faceUp;

	// Use this for initialization
	void Start () {
        objectPooler = ObjectPooling.Instance;
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void flipStatus(bool isTopShowing)
    {
        faceUp = isTopShowing;
        //Debug.Log("Top Player: " + topPlayer.transform.position);
        topPlayer.GetComponent<NavMeshAgent>().isStopped = true;
        //topPlayer.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        //bottomPlayer.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        bottomPlayer.GetComponent<NavMeshAgent>().isStopped = true;
        DeactivateAll("Spell");
    }

    public void DeactivateAll(string tag)
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag(tag) ){
            objectPooler.ReQueue(obj, tag);
        }
    }
}
