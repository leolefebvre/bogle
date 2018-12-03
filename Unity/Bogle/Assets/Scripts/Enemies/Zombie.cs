using System.Collections;
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
	
	// Update is called once per frame
	void Update () {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if(!isDead && GameManager.Instance.currentGameState == GameState.arena)
        {
            navAgent.destination = CrabControler.Instance.transform.position;
        }
    }

    public override void Stop()
    {
        navAgent.enabled = false;

        base.Stop();
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
