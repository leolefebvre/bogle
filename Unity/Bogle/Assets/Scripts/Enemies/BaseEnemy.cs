using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int baseHealth = 1;
    protected int currentHealth;

    public float deathAnimationDuration;
    public Animator animator;
    public ShakeTypes shakeOnDeath;

    private bool isDead = false;
    
	// Use this for initialization
	void Start () {
        currentHealth = baseHealth;
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


    public virtual void Die()
    {
        if(isDead)
        {
            return;
        }

        isDead = true;

        GetComponent<Collider>().enabled = false;
        CameraShakeControler.Instance.LaunchShake(shakeOnDeath);
        animator.SetTrigger("DeathTrigger");

        StartCoroutine(DeleteCharacter());
    }

    IEnumerator DeleteCharacter()
    {
        yield return new WaitForSeconds(deathAnimationDuration);

        Destroy(gameObject);
    }
}
