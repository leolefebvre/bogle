using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int startedHealth = 1;

    protected int currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = startedHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeHit(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    public void Die()
    {
        CameraShakeControler.Instance.LaunchShake(ShakeTypes.big);
        Destroy(gameObject);
    }
}
