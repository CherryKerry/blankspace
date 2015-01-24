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

    void OnEnable ()
    {
        Manager.OnEvent += Manager_OnEvent;
    }

    void Manager_OnEvent(string keyWord, string word)
    {
        if (keyWord == Constants.HouseType.Keyword)
        {
            switch(word)
            {
                case Constants.HouseType.Cottage:
                    GameObject gameObject = Instantiate(Resources.Load("/Prefabs/Cottage")) as GameObject;
                    Vector3 loadPosition = camera.ViewportToWorldPoint(new Vector3(0.8f, 0.8f, 0));
                    gameObject.transform.position = loadPosition;
                    break;
                case Constants.HouseType.House:
                    break;
                case Constants.HouseType.Mansion:
                    break;
            }
        }
    }

    void OnDisable()
    {
        Manager.OnEvent -= Manager_OnEvent;
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
        prefab.GetComponent<Rigidbody2D>().gravityScale = 8f;
        prefab.AddComponent<BoxCollider2D>();
    }

    public void DeSpawn(string str)
    {
        Destroy(gameObject);
    }
}
