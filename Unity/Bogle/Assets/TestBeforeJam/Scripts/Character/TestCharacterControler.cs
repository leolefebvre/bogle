using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacterControler : MonoBehaviour
{
    [Header("Characters Parameters")]
    public float characterSpeed = 5.0f;

    [Space(20f)]
    [Header("Internal logic Parameter DON'T TOUCH")]
    public float inputX = 0f;
    public float inputY = 0f;
    public bool isMoving = false;
    public Vector3 lastMoveVector;
    
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
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    private void ManageMovement()
    {
        Vector3 moveVector = new Vector3(inputX, inputY, 0f);
        moveVector = moveVector.normalized * characterSpeed * Time.deltaTime;
        transform.Translate(moveVector);

        isMoving = (inputX != 0f || inputY != 0f);

        if(isMoving)
        {
            lastMoveVector = moveVector;
        }
    }

    private void ManageAnimations()
    {
        characterAnimator.SetBool("isMoving", isMoving);
        characterAnimator.SetFloat("lastInputX", lastMoveVector.x);
        characterAnimator.SetFloat("lastInputY", lastMoveVector.y);
    }
}
