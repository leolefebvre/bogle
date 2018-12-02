﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : BaseEnemy
{
    public int AttackDamage = 1;

    private NavMeshAgent _navAgent;
    public NavMeshAgent navAgent
    {
        get
        {
            if(_navAgent == null)
            {
                _navAgent = GetComponent<NavMeshAgent>();
            }
            return _navAgent;
        }
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if(!isDead)
        {
            navAgent.destination = CrabControler.Instance.transform.position;
        }
    }

    public override void Die()
    {
        navAgent.enabled = false;

        base.Die();
    }

    public void DamagePlayer()
    {
        CrabControler.Instance.TakeHit(AttackDamage);
        Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DamagePlayer();
        }
    }
}
