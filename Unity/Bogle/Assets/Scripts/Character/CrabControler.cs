﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabControler : Singleton<CrabControler>
{
    #region externalParameters

    [Header("Characters Movements Parameters")]
    public float walkingSpeed = 5.0f;
    public float rotatingSpeed = 5.0f;

    public Vector3 walkAxis = Vector3.right;
    public Vector3 rotationAxis = Vector3.forward;

    [Header("Characters Shoot Parameters")]
    public GameObject projectilePrefab;
    public float baseRange = 100f;
    [Tooltip("In number of bullets per seconds")]
    public float baseFireRate = 5f;

    #endregion

    #region references

    [Space(20f)]
    [Header("References")]
    public Animator walkAnimator;
    public CannonControler cannon1;
    public CannonControler cannon2;

    #endregion

    #region internal logic parameters

    [Space(20f)]
    [Header("Internal logic Parameter DON'T TOUCH")]
    public float walkingInput = 0f;
    public float rotatingInput = 0f;
    public float shootInput = 0f;
    public bool isMoving = false;

    public float currentRange = 100f;
    public float currentFireRate = 1.0f;

    public float lastTimeShots = 0f;
    public float timeBetweenShots
    {
        get { return 1f / currentFireRate; }
    }

    public float rotationSide = 1f;

    #endregion

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        currentRange = baseRange;
        currentFireRate = baseFireRate;
    }

    // Update is called once per frame
    void Update()
    {
        ManageInputs();

        ManageMovement();
        ManageShooting();

        ManageAnimations();
    }

    #region inputs

    private void ManageInputs()
    {
        walkingInput = Input.GetAxis("Horizontal");
        rotatingInput = Input.GetAxis("Vertical");
        shootInput = Input.GetAxis("Fire1");
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


        // if press up and right or press down and left, then rotate counter clockwise
        // else rotate clockwise
        if((rotatingInput < 0f && walkingInput > 0f) || (rotatingInput > 0f && walkingInput < 0f))
        {
            rotationSide = -1f;
        }
        else
        {
            rotationSide = 1f;
        }

        float rotationAngle = rotationSide * rotatingSpeed * Time.deltaTime;

        transform.Rotate(rotationAxis, rotationAngle);
    }

    private void EngageWalking()
    {
        Vector3 walkingVector = walkAxis * walkingInput * walkingSpeed * Time.deltaTime;

        transform.Translate(walkingVector);
    }

    #endregion

    #region shooting

    private void ManageShooting()
    {
        if(shootInput != 0f && (Time.time - lastTimeShots) > timeBetweenShots)
        {
            cannon1.Fire(currentRange);
            cannon2.Fire(currentRange);
            lastTimeShots = Time.time;

            CameraShakeControler.Instance.LaunchShake(ShakeTypes.fireShake);
        }
    }

    #endregion

    #region Animations

    private void ManageAnimations()
    {
        walkAnimator.SetBool("isMoving", isMoving);
    }

    #endregion



    public void TakeHit(int damage)
    {

    }
}
