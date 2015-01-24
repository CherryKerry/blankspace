using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabsSpawner : MonoBehaviour {

    //public Transform pHouse;

    public GameObject[] blockPrefabs;
    public Dictionary<string, GameObject> blockTypes;


    void Start()
    {
        blockTypes = new Dictionary<string, GameObject>();

        foreach (GameObject prefab in blockPrefabs)
        {
            blockTypes[prefab.name] = prefab;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Spawn("cottage");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Spawn("house");
        }
	}

    void Spawn(string str)
    {
        GameObject prefab = Instantiate(blockTypes[str], new Vector3(Random.Range(-4, 4), Random.Range(-3, 3), 0), Quaternion.identity) as GameObject;
        prefab.AddComponent<Rigidbody2D>();
        prefab.GetComponent<Rigidbody2D>().gravityScale = 10f;
        prefab.AddComponent<BoxCollider2D>();
    }
}
