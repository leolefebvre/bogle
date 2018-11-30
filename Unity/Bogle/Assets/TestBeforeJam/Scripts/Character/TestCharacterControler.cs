using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingStates
{
    idle = 0,
    walking = 1,
    rotating = 2
}

public class TestCharacterControler : MonoBehaviour
{
    [Header("Characters Parameters")]
    public float walkingSpeed = 5.0f;
    public float rotatingSpeed = 5.0f;

    public Vector3 rotationAxis = Vector3.up;

    [Space(20f)]
    [Header("Internal logic Parameter DON'T TOUCH")]
    public float walkingInput = 0f;
    public float rotatingInput = 0f;
    public bool isMoving = false;

    public MovingStates currentMovingState = MovingStates.idle;
    
    private Animator _characterAnimator;
    public Animator characterAnimator
    {
        get
        {
            if (_characterAnimator == null)
            {
                _characterAnimator = GetComponent<Animator>();
            }
            return _characterAnimator;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ManageInputs();
        ManageMovement();
        ManageAnimations();

    }

    private void ManageInputs()
    {
        walkingInput = Input.GetAxis("Horizontal");
        rotatingInput = Input.GetAxis("Vertical");
    }

    private void ManageMovement()
    {
        if(walkingInput != 0)
        {
            EngageWalking();
            if(rotatingInput != 0)
            {
                EngageRotation();
            }
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void ManageAnimations()
    {
        characterAnimator.SetBool("isMoving", isMoving);
    }

    private void EngageRotation()
    {
        float rotationAngle = rotatingInput * rotatingSpeed * Time.deltaTime;

        transform.Rotate(rotationAxis, rotationAngle);
    }

    private void EngageWalking()
    {
        Vector3 walkingVector = Vector3.right * walkingInput * walkingSpeed * Time.deltaTime;

        transform.Translate(walkingVector);
    }
}
