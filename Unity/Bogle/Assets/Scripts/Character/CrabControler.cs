﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabControler : Singleton<CrabControler>
{
    #region externalParameters

    [Header("Characters Movements Parameters")]
    public float baseWalkingSpeed = 5.0f;
    public float baseRotatingSpeed = 5.0f;

    public Vector3 walkAxis = Vector3.right;
    public Vector3 rotationAxis = Vector3.forward;

    [Header("Characters Shoot Parameters")]
    public GameObject projectilePrefab;
    public float baseRange = 100f;
    [Tooltip("In number of bullets per seconds")]
    public float baseFireRate = 5f;
    public ShakeTypes shakeOnFire = ShakeTypes.fireShake;

    [Header("Characters Health Parameters")]
    public int baseHealth = 3;
    public float invincibilityDuration = 1.0f;
    public ShakeTypes shakeOnTakingHits = ShakeTypes.playerTakesHitShake;

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
    public bool isInvincible = false;

    private Vector3 basePosition;
    private Quaternion baseRotation;
    
    public bool isDead
    {
        get { return currentHealth <= 0; }
    }

    public int currentHealth = 3;
    public float currentRange = 100f;
    public float currentFireRate = 1.0f;
    public float currentWalkingSpeed = 5.0f;
    public float currentRotatingSpeed = 5.0f;

    private float lastTimeShots = 0f;
    public float timeBetweenShots
    {
        get { return 1f / currentFireRate; }
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        basePosition = transform.position;
        baseRotation = transform.rotation;
        Initialize();
    }

    private void Initialize()
    {
        currentRange = baseRange;
        currentFireRate = baseFireRate;
        currentHealth = baseHealth;
        currentWalkingSpeed = baseWalkingSpeed;
        currentRotatingSpeed = baseRotatingSpeed;

        walkingInput = 0f;
        rotatingInput = 0f;
        shootInput = 0f;
        isMoving = false;
        isInvincible = false;
        lastTimeShots = 0f;
    }

    public void Reset()
    {
        Initialize();
        transform.position = basePosition;
        transform.rotation = baseRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead || GameManager.Instance.currentGameState != GameState.arena)
        {
            return;
        }

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
         float rotationSide = 1f;

        // if press up and right or press down and left, then rotate counter clockwise
        // else rotate clockwise
        if((rotatingInput < 0f && walkingInput > 0f) || (rotatingInput > 0f && walkingInput < 0f))
        {
            rotationSide = -1f;
        }
        float rotationAngle = rotationSide * currentRotatingSpeed * Time.deltaTime;

        transform.Rotate(rotationAxis, rotationAngle);
    }

    private void EngageWalking()
    {
        Vector3 walkingVector = walkAxis * walkingInput * currentWalkingSpeed * Time.deltaTime;

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

            CameraShakeControler.Instance.LaunchShake(shakeOnFire);
        }
    }

    #endregion

    #region Animations

    private void ManageAnimations()
    {
        walkAnimator.SetBool("isMoving", isMoving);
        walkAnimator.SetBool("isInvincible", isInvincible);
    }

    #endregion

    #region Health and damage taking

    public void TakeHit(int damage)
    {
        if(isInvincible)
        {
            return;
        }

        Debug.Log("Taking hits");
        currentHealth -= damage;

        HealthDisplayManager.Instance.RemoveOneHeart();
        CameraShakeControler.Instance.LaunchShake(shakeOnTakingHits);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        isInvincible = true;

        StartCoroutine(StopInvicibility());
    }

    IEnumerator StopInvicibility()
    {
        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
    }

    public void Die ()
    {
        GameManager.Instance.ManageCharacterDeath();
    }

    #endregion
}
