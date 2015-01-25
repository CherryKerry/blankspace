using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabsSpawner : MonoBehaviour
{

		//public Transform pHouse;

		public Collider2D characterCollider;
		private bool trackThought = false;
		private GameObject prefabTrack;
		private bool fadeOut = false;

		void OnEnable ()
		{
				Manager.OnEvent += Manager_OnEvent;
		}

		void Manager_OnEvent (string keyWord, string word)
		{
				Debug.Log (keyWord + " " + word);
				Vector3 loadPosition;
				if (!String.IsNullOrEmpty (word)) {
						switch (keyWord) {
						case Constants.HouseType.Keyword:
								loadPosition = Camera.main.ViewportToWorldPoint (new Vector3 (0.8f, 0.8f, 0));
								loadPosition.z = 0.2f; //Placing behind character
								StartCoroutine (SmokeSpawn (word, loadPosition));    
                
								break;
						case Constants.AnimalType.Keyword:
								loadPosition = Camera.main.ViewportToWorldPoint (new Vector3 (0.8f, 0.8f, 0));
								loadPosition.z = 0.2f; //Placing behind character
								StartCoroutine (SmokeSpawn (word, loadPosition));   
								break;

						case Constants.BlankType.Keyword:
								loadPosition = Camera.main.ViewportToWorldPoint (new Vector3 (0.8f, 0.8f, 0));
								loadPosition.z = 0.2f; //Placing behind character
								TrackerSpawn (word + "_Thought", loadPosition);
								break;
	
						case Constants.VisitType.Keyword:
								loadPosition = GameObject.Find ("RedRidingHoodAlpha").transform.position;//.main.ViewportToWorldPoint (new Vector3 (0.8f, 0.8f, 0));
								loadPosition.x = loadPosition.x + 2;
								loadPosition.y = loadPosition.y + 2;
								trackThought = true;
								//loadPosition = Camera.main.ViewportToWorldPoint (new Vector3 (0.8f, 0.8f, 0));
								loadPosition.z = 0.2f; //Placing behind character
								TrackerSpawn (word + "_Thought", loadPosition);
								break;
						case Constants.WeaponType.Keyword:
								loadPosition = Camera.main.ViewportToWorldPoint (new Vector3 (0.8f, 0.8f, 0));
								loadPosition.z = 0.2f; //Placing behind character
								TrackerSpawn (word + "_Thought", loadPosition);
								break;
						case Constants.BodyPart.Keyword:
								loadPosition = Camera.main.ViewportToWorldPoint (new Vector3 (0.8f, 0.8f, 0));
								loadPosition.z = 0.2f; //Placing behind character
								TrackerSpawn (word + "_Thought", loadPosition);
								break;
						case Constants.Action.Keyword:
								loadPosition = GameObject.Find ("RedRidingHoodAlpha").transform.position;//.main.ViewportToWorldPoint (new Vector3 (0.8f, 0.8f, 0));
								loadPosition.x = loadPosition.x + 10;
								loadPosition.y = loadPosition.y + 10;
								loadPosition = Camera.main.ViewportToWorldPoint (new Vector3 (0.8f, 0.8f, 0));
								loadPosition.z = 0.2f; //Placing behind character
								TrackerSpawn (word + "_Thought", loadPosition);
								break;
						}
				}
		}
	
		void OnDisable ()
		{
				Manager.OnEvent -= Manager_OnEvent;
		}

		void Update ()
		{
				if (trackThought) {
						TrackThought ();
						Invoke ("SetFadeOut", 1.2f);
				}
				if (fadeOut) {
						//FadeOut ();
				}
		}

		void FadeOut ()
		{		
				Color color = prefabTrack.renderer.material.color;
				color.a -= 0.1f;
				prefabTrack.renderer.material.color = color;
		}

		void SetFadeOut ()
		{
				fadeOut = true;
				Invoke ("DestroyTracker", 1.2f);
		}

		void DestroyTracker ()
		{
				trackThought = false;
				fadeOut = false;
				
				Destroy (prefabTrack);
				
		}

		void TrackThought ()
		{
				
				Vector3 v3 = GameObject.Find ("RedRidingHoodAlpha").transform.position;
				v3.x = v3.x + 2;
				v3.y = v3.y + 2;
				prefabTrack.transform.position = v3;
		}
	
		void TrackerSpawn (string str, Vector3 location)
		{
				//Debug.Log ("str: " + str + " @ " + location);
				Destroy (prefabTrack);
				prefabTrack = Instantiate (Resources.Load (str), location, Quaternion.identity) as GameObject;
				prefabTrack.AddComponent<Rigidbody2D> ();
				prefabTrack.GetComponent<Rigidbody2D> ().gravityScale = 8f;
				prefabTrack.AddComponent<BoxCollider2D> ();
				Physics2D.IgnoreCollision (prefabTrack.GetComponent<BoxCollider2D> (), characterCollider);
				trackThought = true;
		}

		void NormalSpawn (string str, Vector3 location)
		{
				GameObject prefab = Instantiate (Resources.Load (str), location, Quaternion.identity) as GameObject;
				prefab.AddComponent<Rigidbody2D> ();
				prefab.GetComponent<Rigidbody2D> ().gravityScale = 8f;
				prefab.AddComponent<BoxCollider2D> ();
				Physics2D.IgnoreCollision (prefab.GetComponent<BoxCollider2D> (), characterCollider);
		}

		IEnumerator SmokeSpawn (string str, Vector3 location)
		{
				GameObject smokePuffs = Instantiate (Resources.Load ("SmokePuffs"), location, Quaternion.identity) as GameObject;
				yield return new WaitForSeconds (0.85f);
				GameObject prefab = Instantiate (Resources.Load (str), location, Quaternion.identity) as GameObject;

				prefab.AddComponent<Rigidbody2D> ();
				prefab.AddComponent<BoxCollider2D> ();
				Physics2D.IgnoreCollision (prefab.GetComponent<BoxCollider2D> (), characterCollider);
		}

		public void DeSpawn (string str)
		{
				Destroy (gameObject);
		}
}
