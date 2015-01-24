using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class CharacterColourChangeController : MonoBehaviour
{

    private Animator animator;
    private string characterColourAnimParam = "CHARACTER_COLOUR";
    public CharacterColour characterColour;

	// Use this for initialization
	void Start ()
	{
	    characterColour = CharacterColour.RED;
	    animator = GetComponent<Animator>();
        //TODO: Bind to event in Sender.Message += OnEvent(message);
	}

    void OnEvent()
    {
        /*
         * 0 - RED
         * 1 - BLUE
         * 2 - GREEN
         * 3 - HOT PINK
         */
        switch (characterColour)
        {
            case CharacterColour.RED:
                animator.SetFloat(characterColourAnimParam,0.0f);
                break;
            case CharacterColour.BLUE:
                animator.SetFloat(characterColourAnimParam, 1.0f);
                break;
            case CharacterColour.GREEN:
                animator.SetFloat(characterColourAnimParam, 2.0f);
                break;
            case CharacterColour.HOTPINK:
                animator.SetFloat(characterColourAnimParam, 3.0f);
                break;
            default:
                break;
        };
    }
	
	// Update is called once per frame
	void Update ()
	{
	    OnEvent();
	}
}

public enum CharacterColour
{
    RED,
    BLUE,
    GREEN,
    HOTPINK
}