using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : BaseEnemy
{
    [Header("Turret Parameters")]

    public float maxRotatingSpeed = 5f;
    public float maxRange = 40f;

    public float invicibilityTimeMin = 0.5f;
    public float invicibilityTimeMax = 1f;

    public float timeBeforeShooting = .5f;
    public AnimationClip attackAnimation;

    public GameObject turretProjectile;
    public Transform shootingPositon;


    [Header("Internal Logic Parameters DON'T TOUCH")]
    public bool isInvincible = true;
    public float currentInvicibilityDuration = 0f;
    public bool isTurretActive = false;

    // Update is called once per frame
    void Update ()
    {
        LookAtPlayer();
    }

    protected override void Initialize()
    {
        base.Initialize();
        Reset();
    }

    private void Reset()
    {
        isInvincible = true;
        currentInvicibilityDuration = 0f;
    }


    public void ActivateTurretBehavior()
    {
        if(isTurretActive)
        {
            return;
        }
        isTurretActive = true;

        currentInvicibilityDuration = Random.Range(invicibilityTimeMin, invicibilityTimeMax);
        StartCoroutine(WaitBeforeLaunchFirstAttack());
    }

    public void DeActivateTurretBehavior()
    {
        if (!isTurretActive)
        {
            return;
        }
        isTurretActive = false;

        StopAllCoroutines();
        Reset();
    }

    private void LookAtPlayer()
    {
        if(!isTurretActive)
        {
            return;
        }

        Vector3 lookPostion = CrabControler.Instance.transform.position - transform.position;
        lookPostion.y = 0f;

        Quaternion rotation = Quaternion.LookRotation(lookPostion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * maxRotatingSpeed);
    }

    private void LaunchShootSequence()
    {
        if(!isTurretActive)
        {
            return;
        }

        animator.SetTrigger("AttackTrigger");

        isInvincible = false;

        currentInvicibilityDuration = Random.Range(invicibilityTimeMin, invicibilityTimeMax);

        StartCoroutine(WaitBeforeShooting());
        StartCoroutine(WaitBeforeLaunchNextAttack());
    }

    IEnumerator WaitBeforeShooting()
    {
        yield return new WaitForSeconds(timeBeforeShooting);

        if (isTurretActive)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!isTurretActive)
        {
            return;
        }

        GameObject projectile = Instantiate(turretProjectile, shootingPositon.position, shootingPositon.rotation);
        projectile.GetComponent<Projectile>().Launch(maxRange, Team.Enemy);
    }

    IEnumerator WaitBeforeLaunchNextAttack()
    {
        yield return new WaitForSeconds(currentInvicibilityDuration + attackAnimation.length);

        if (isTurretActive)
        {
            LaunchShootSequence();
        }
    }

    IEnumerator WaitBeforeLaunchFirstAttack()
    {
        yield return new WaitForSeconds(currentInvicibilityDuration);

        if (isTurretActive)
        {
            LaunchShootSequence();
        }
    }

    IEnumerator WaitBeforeInvincibilty()
    {
        yield return new WaitForSeconds(attackAnimation.length);

        if (isTurretActive)
        {
            isInvincible = true;
        }
    }


    public override void TakeHit(int damage)
    {
        if (isInvincible)
        {
            return;
        }

        base.TakeHit(damage);
    }
}
