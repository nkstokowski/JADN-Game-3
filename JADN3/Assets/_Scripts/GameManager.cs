using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {

    public GameObject topPlayer;
    public GameObject bottomPlayer;
    public Vector3 playerPosition;
    public Vector3 topPlayerPosition;
    public Vector3 bottomPlayerPosition;
    public FlipScript flipper;

    private Block[] blocks;
    private NavMeshAgent topPlayerAgent;
    private NavMeshAgent bottomPlayerAgent;

    ObjectPooling objectPooler;

    public bool faceUp;

	// Use this for initialization
	void Start () {
        objectPooler = ObjectPooling.Instance;
        blocks = FindObjectsOfType<Block>();
        topPlayerAgent = topPlayer.GetComponent<NavMeshAgent>();
        bottomPlayerAgent = bottomPlayer.GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void StartFlip(bool isTopShowing)
    {
        faceUp = isTopShowing;
        //Debug.Log("Top Player: " + topPlayer.transform.position);
        topPlayerAgent.isStopped = true;
        //topPlayer.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        //bottomPlayer.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        bottomPlayerAgent.isStopped = true;
        DeactivateAll("Spell");
        foreach(Block b in blocks)
        {
            b.ResetPosition();
        }
        topPlayerAgent.enabled = false;
        bottomPlayerAgent.enabled = false;
        //topPlayerPosition = topPlayer.transform.localPosition;
        //bottomPlayerPosition = bottomPlayer.transform.localPosition;
    }

    public void EndFlip()
    {
        topPlayerAgent.enabled = true;
        bottomPlayerAgent.enabled = true;
        //bottomPlayer.transform.localPosition = bottomPlayerPosition;
        //topPlayer.transform.localPosition = topPlayerPosition;
    }

    public void DeactivateAll(string tag)
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag(tag) ){
            objectPooler.ReQueue(obj, tag);
        }
    }
}
