using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControler : MonoBehaviour
{
    private GameObject _projectilePrefab;
    public GameObject projectilePrefab
    {
        get
        {
            if (_projectilePrefab == null)
            {
                _projectilePrefab = CrabControler.Instance.projectilePrefab;
            }
            return _projectilePrefab;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Fire (float maxRange)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        projectile.GetComponent<Projectile>().Launch(maxRange, Team.Player);
    }
}
