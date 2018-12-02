using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenManager : Singleton<DeathScreenManager>
{
    public GameObject deathUiCanvas;

	// Use this for initialization
	void Start () {
        deathUiCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LaunchDeathUI()
    {
        deathUiCanvas.SetActive(true);
    }

    public void RestartClicked()
    {
        Debug.Log("RestartClicked");
    }
}
