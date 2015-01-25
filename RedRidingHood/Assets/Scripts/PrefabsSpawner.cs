using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabsSpawner : MonoBehaviour {

    //public Transform pHouse;

    public Collider2D characterCollider;

    void OnEnable ()
    {
        Manager.OnEvent += Manager_OnEvent;
    }

    void Manager_OnEvent(string keyWord, string word)
    {
        Debug.Log(keyWord + " " + word);
        Vector3 loadPosition;
        if (!String.IsNullOrEmpty(word))
        {
            switch (keyWord)
            {
                case Constants.HouseType.Keyword:
                    loadPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.8f, 0.8f, 0));
                    loadPosition.z = 0.2f; //Placing behind character
                    StartCoroutine(SmokeSpawn(word, loadPosition));    
                
                    break;
                case Constants.AnimalType.Keyword:
                    loadPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.8f, 0.8f, 0));
                    loadPosition.z = 0.2f; //Placing behind character
                    StartCoroutine(SmokeSpawn(word, loadPosition));   
                    //NormalSpawn(word, loadPosition);
                    break;
                case Constants.BlankType.Keyword:
                    //new spawn
                    break;
            }
        }
    }

    void OnDisable()
    {
        Manager.OnEvent -= Manager_OnEvent;
    }
	

    void NormalSpawn(string str, Vector3 location)
    {
        GameObject prefab = Instantiate(Resources.Load(str), location, Quaternion.identity) as GameObject;
        prefab.AddComponent<Rigidbody2D>();
        prefab.GetComponent<Rigidbody2D>().gravityScale = 8f;
        prefab.AddComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(prefab.GetComponent<BoxCollider2D>(), characterCollider);
    }

    IEnumerator SmokeSpawn(string str, Vector3 location)
    {
        GameObject smokePuffs = Instantiate(Resources.Load("SmokePuffs"), location, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(0.85f);
        GameObject prefab = Instantiate(Resources.Load(str), location, Quaternion.identity) as GameObject;

        prefab.AddComponent<Rigidbody2D>();
        prefab.AddComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(prefab.GetComponent<BoxCollider2D>(), characterCollider);
    }

    public void DeSpawn(string str)
    {
        Destroy(gameObject);
    }
}
