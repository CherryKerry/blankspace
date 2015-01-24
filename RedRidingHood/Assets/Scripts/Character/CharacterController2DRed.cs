using UnityEngine;
using System.Collections;

public class CharacterController2DRed : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    public float horizontalForce = 10.0f;
    string redStateAnimatorParam = "CHARACTER_STATE";

    public RedRidingHoodState redRidingHoodState;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //redRidingHoodState = RedRidingHoodState.WALKING;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveRed(redRidingHoodState);
        AnimateMatilda(redRidingHoodState);
    }

    private void OnCollisionEnter2D(Collision2D collission)
    {

    }


    void OnCollisionStay2D(Collision2D collission)
    {

    }

    void OnCollisionExit2D(Collision2D collission)
    {

    }



    void MoveRed(RedRidingHoodState redRidingHoodState)
    {
        switch (redRidingHoodState)
        {
            case RedRidingHoodState.WALKING:
                rigidbody2D.velocity = new Vector2(horizontalForce, rigidbody2D.velocity.y);
                break;
            case RedRidingHoodState.IDLE:
                break;
        }
    }

    /*
     * 0 - Idle
     * 1 - Walk
     */
    void AnimateMatilda(RedRidingHoodState redRidingHoodState)
    {
        switch (redRidingHoodState)
        {
            case RedRidingHoodState.WALKING:
                animator.SetInteger(redStateAnimatorParam, 1);
                break;
            case RedRidingHoodState.IDLE:
                animator.SetInteger(redStateAnimatorParam, 0);
                break;
        } 
    }

    public enum RedRidingHoodState
    {
        WALKING,
        IDLE,
    }
}
