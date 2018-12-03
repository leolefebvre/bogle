using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Player,
    Enemy
}

public class Projectile : MonoBehaviour
{
    public Team projectileTeam = Team.Player;

    public float speed = 20.0f;
    public float maxDistance = 100.0f;
    public int projectileDamage = 1;

    public float hitAnimationTime = 0.5f;
    public Animator animator;

    public bool isLaunched = false;
    public bool hasAlreadyHit = false;

    public float timeToLive = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Launch(float overrideMaxDistance, Team team)
    {
        maxDistance = overrideMaxDistance;
        projectileTeam = team;
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
        if(hasAlreadyHit)
        {
            return;
        }

        hasAlreadyHit = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        animator.SetTrigger("projectileHits");

        StartCoroutine(DeleteProjectile());
    }
    
    IEnumerator DeleteProjectile()
    {
        yield return new WaitForSeconds(hitAnimationTime);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            ProjectileHits();
        }

        if(projectileTeam == Team.Player && other.tag == "Ennemy")
        {
            Debug.Log(name + " hits " + other.name);

            other.GetComponent<BaseEnemy>().TakeHit(projectileDamage);
            ProjectileHits();
        }

        if (projectileTeam == Team.Enemy && other.tag == "Player")
        {
            CrabControler.Instance.TakeHit(projectileDamage);
            ProjectileHits();
        }
    }
}
