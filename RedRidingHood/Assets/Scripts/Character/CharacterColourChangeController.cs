using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class CharacterColourChangeController : MonoBehaviour
{

    private Animator animator;
    private string characterColourAnimParam = "CHARACTER_COLOUR";

	// Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();
        animator.SetFloat(characterColourAnimParam, -1.0f);
	}

    void OnEnable()
    {
        Manager.OnEvent += Manager_OnEvent;
    }

    void OnDisable()
    {
        Manager.OnEvent -= Manager_OnEvent;
    }

    void Manager_OnEvent(string keyWord, string word)
    {

        if (keyWord == Constants.HoodColour.Keyword)
        {
            //Debug.Log(keyWord + ", " + word);
            /*
             * 0 - RED
             * 1 - BLUE
             * 2 - GREEN
             * 3 - HOT PINK
             */
            switch (word)
            {
                case Constants.HoodColour.Red:
                    animator.SetFloat(characterColourAnimParam, 0.0f);
                    break;
                case Constants.HoodColour.Blue:
                    animator.SetFloat(characterColourAnimParam, 1.0f);
                    break;
                case Constants.HoodColour.Green:
                    animator.SetFloat(characterColourAnimParam, 2.0f);
                    break;
                case Constants.HoodColour.HotPink:
                    animator.SetFloat(characterColourAnimParam, 3.0f);
                    break;
                default:
                    animator.SetFloat(characterColourAnimParam, 0.0f);
                    break;
            };  
        }
    }

	
	// Update is called once per frame
	void Update ()
	{
	}
}