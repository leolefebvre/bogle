using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int startedHealth = 1;

    protected int _currentHealth;

	// Use this for initialization
	void Start () {
        _currentHealth = startedHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
