using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingStates
{
    idle = 0,
    walking = 1,
    rotating = 2
}

public class CrabControler : Singleton<CrabControler>
{
    #region externalParameters

    [Header("Characters Parameters")]
    public float walkingSpeed = 5.0f;
    public float rotatingSpeed = 5.0f;

    public Vector3 walkAxis = Vector3.right;
    public Vector3 rotationAxis = Vector3.forward;

    #endregion

    #region references

    [Space(20f)]
    [Header("References")]
    public Animator walkAnimator;
    public Transform ShootPosition;

    #endregion

    #region internal logic parameters

    [Space(20f)]
    [Header("Internal logic Parameter DON'T TOUCH")]
    public float walkingInput = 0f;
    public float rotatingInput = 0f;
    public bool isMoving = false;

    public MovingStates currentMovingState = MovingStates.idle;

    #endregion

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ManageInputs();
        ManageMovement();
        ManageAnimations();

    }

    #region inputs

    private void ManageInputs()
    {
        walkingInput = Input.GetAxis("Horizontal");
        rotatingInput = Input.GetAxis("Vertical");
    }

    #endregion

    #region movements

    private void ManageMovement()
    {
        if (walkingInput != 0)
        {
            EngageWalking();
            if (rotatingInput != 0)
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

    private void EngageRotation()
    {
        float rotationAngle = rotatingInput * rotatingSpeed * Time.deltaTime;

        transform.Rotate(rotationAxis, rotationAngle);
    }

    private void EngageWalking()
    {
        Vector3 walkingVector = walkAxis * walkingInput * walkingSpeed * Time.deltaTime;

        transform.Translate(walkingVector);
    }

    #endregion

    #region Animations

    private void ManageAnimations()
    {
        walkAnimator.SetBool("isMoving", isMoving);
    }

    #endregion
}
