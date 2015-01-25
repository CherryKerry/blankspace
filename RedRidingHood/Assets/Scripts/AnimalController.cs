using System;
using UnityEngine;
using System.Collections;

public class AnimalController : MonoBehaviour {

	// Use this for initialization
    GameObject character;
    private CharacterController2DRed characterController;
    private GameObject currentAnimal;

	void Start () {
        character = GameObject.Find("RedRidingHoodAlpha");
        characterController = character.GetComponent<CharacterController2DRed>();
	    characterController.StopCharacter();
        
	}


    void OnEnable()
    {
        Manager.OnEvent += Manager_OnEvent;
    }

    void Manager_OnEvent(string keyWord, string word)
    {
        Debug.Log(keyWord + " " + word);
        if (!String.IsNullOrEmpty(word))
        {
            switch (keyWord)
            {
                case Constants.HouseType.Keyword:
                    break;
                case Constants.AnimalType.Keyword:
                    break;
                case Constants.BlankType.Keyword:
                    //Behavior of antagonist
                    Debug.LogError(word);
                    if (word == Constants.BlankType.Bed || word == Constants.BlankType.Kitchen ||
                        word == Constants.BlankType.Clothes)
                    {
                        Debug.Log(word);
                        characterController.StartCharacter();
                        Destroy(this.gameObject);
                    }
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
	
	}
}
