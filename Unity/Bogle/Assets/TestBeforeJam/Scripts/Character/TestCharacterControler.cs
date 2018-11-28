using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacterControler : MonoBehaviour
{
    public float characterSpeed = 5.0f;

    private float inputX = 0f;
    private float inputY = 0f;
    private bool isMoving = false;
    
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
        transform.Translate(inputX * characterSpeed * Time.deltaTime, inputY * characterSpeed * Time.deltaTime, 0f);

        isMoving = (inputX != 0f || inputY != 0f);
    }

    private void ManageAnimations()
    {
        characterAnimator.SetBool("isMoving", isMoving);
        characterAnimator.SetFloat("inputX", inputX);
        characterAnimator.SetFloat("inputY", inputY);
    }
}
