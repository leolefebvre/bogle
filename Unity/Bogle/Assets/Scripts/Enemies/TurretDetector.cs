using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDetector : MonoBehaviour
{
    public Turret linkedTurret;
    private bool isActive = false;

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if(!isActive)
        {
            if(GameManager.Instance.currentGameState == GameState.arena)
            {
                ActivateTurretDetector();
            }
        }
    }

    public void ActivateTurretDetector()
    {
        GetComponent<Collider>().enabled = true;
        isActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            linkedTurret.ActivateTurretBehavior();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            linkedTurret.DeActivateTurretBehavior();
        }
    }
}
