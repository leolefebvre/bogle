using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Base Ennemy Parameter")]
    public int baseHealth = 1;
    protected int currentHealth;

    public float deathAnimationDuration;
    public Animator animator;
    public ShakeTypes shakeOnDeath;

    public AudioClip onDeathSound;

    protected bool isDead = false;

    public AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected virtual void Initialize()
    {
        currentHealth = baseHealth;
    }

    public virtual void TakeHit(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Stop()
    {
        GetComponent<Collider>().enabled = false;
    }

    public virtual void Die()
    {
        if(isDead)
        {
            return;
        }
        
        isDead = true;

        Stop();

        CameraShakeControler.Instance.LaunchShake(shakeOnDeath);
        animator.SetTrigger("DeathTrigger");
        PlaySound(onDeathSound);

        GameManager.Instance.RegisterDeadEnemy();

        StartCoroutine(DeleteCharacter());
    }

    IEnumerator DeleteCharacter()
    {
        yield return new WaitForSeconds(deathAnimationDuration);

        Destroy(gameObject);
    }

    public void PlaySound(AudioClip soundToPlay)
    {
        if (soundToPlay == null)
        {
            return;
        }

        audioSource.PlayOneShot(soundToPlay);
    }
}
