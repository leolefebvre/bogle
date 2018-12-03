using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDetector : MonoBehaviour
{
    public Turret linkedTurret;


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
