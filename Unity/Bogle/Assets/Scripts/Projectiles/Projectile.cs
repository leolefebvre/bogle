using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20.0f;
    public float maxDistance = 100.0f;

    public int projectileDamage = 1;

    public float hitAnimationTime = 0.5f;
    public Animator animator;

    public bool isLaunched = false;

    public float timeToLive = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Launch(float overrideMaxDistance)
    {
        maxDistance = overrideMaxDistance;
        Launch();
    }

    public void Launch()
    {
        isLaunched = true;

        GetComponent<Rigidbody>().velocity = transform.up * speed;

        StartCoroutine(DeleteProjectileAfterDistanceCovered());
    }

    IEnumerator DeleteProjectileAfterDistanceCovered()
    {
        timeToLive = maxDistance / speed;

        yield return new WaitForSeconds(maxDistance / speed);

        ProjectileHits();
    }

    private void ProjectileHits()
    {
        Debug.Log("My life ends here");

        animator.SetTrigger("projectileHits");

        StartCoroutine(DeleteProjectile());
    }
    
    IEnumerator DeleteProjectile()
    {
        yield return new WaitForSeconds(hitAnimationTime);

        Debug.Log("JK, I am really dead here");

        Destroy(gameObject);
    }
}
