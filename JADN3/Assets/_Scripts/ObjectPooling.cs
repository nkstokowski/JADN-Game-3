using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooling Instance;

    public void Awake()
    {
        Instance = this;
    }

    [Header("Object Pools")]
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Use this for initialization
    void Start () {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public GameObject SpawnFromPool(Vector3 position, Quaternion rotation, string tag)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            if(poolDictionary[tag].Count > 0)
            {
                GameObject objectToSpawn = poolDictionary[tag].Dequeue();

                objectToSpawn.SetActive(true);
				FindObjectOfType<AudioManager> ().PlaySoundWithTag ("LightSide");
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;

                //poolDictionary[tag].Enqueue(objectToSpawn);

                return objectToSpawn;
            }
        }

        return null;

    }

    public void ReQueue(GameObject obj, string tag)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            poolDictionary[tag].Enqueue(obj);
            obj.SetActive(false);
        }
    }

    
}
