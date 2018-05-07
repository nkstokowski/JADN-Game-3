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
    private NavMeshAgent[] enemyAgents;

    ObjectPooling objectPooler;

    public bool faceUp;

	// Use this for initialization
	void Start () {
        objectPooler = ObjectPooling.Instance;
        blocks = FindObjectsOfType<Block>();
        topPlayerAgent = topPlayer.GetComponent<NavMeshAgent>();
        bottomPlayerAgent = bottomPlayer.GetComponent<NavMeshAgent>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyAgents = new NavMeshAgent[enemies.Length];
        int i = 0;
        foreach(GameObject enemy in enemies)
        {
            enemyAgents[i] = enemy.GetComponent<NavMeshAgent>();
            i++;
        }
		FindObjectOfType<AudioManager> ().PlaySoundWithTag ("Ambient");
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void StartFlip(bool isTopShowing)
    {
        faceUp = isTopShowing;
        topPlayerAgent.isStopped = true;

        bottomPlayerAgent.isStopped = true;
        DeactivateAll("Spell");
        foreach(Block b in blocks)
        {
            b.ResetPosition();
        }

        // Disable all navmesh agents
        SetAllAgents(false);
    }

    public void EndFlip()
    {
        // Enable all navmesh agents
        SetAllAgents(true);
    }

    public void SetAllAgents(bool enabledState)
    {
        topPlayerAgent.enabled = enabledState;
        bottomPlayerAgent.enabled = enabledState;
        foreach (NavMeshAgent enemy in enemyAgents)
        {
            enemy.enabled = enabledState;
        }
    }

    public void DeactivateAll(string tag)
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag(tag) ){
            objectPooler.ReQueue(obj, tag);
        }
    }

    public void LoadLevel(string name){
        Initiate.Fade(name,Color.black,0.7f);
    }
    public void Quit() {
        Application.Quit();
    }
}
