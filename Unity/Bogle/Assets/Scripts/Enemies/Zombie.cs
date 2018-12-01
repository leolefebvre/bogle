using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : BaseEnemy
{
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
        navAgent.destination = CrabControler.Instance.transform.position;
    }
}
