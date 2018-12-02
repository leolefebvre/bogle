using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenManager : Singleton<DeathScreenManager>, IResetable
{
    public GameObject deathUiCanvas;

	// Use this for initialization
	void Start () {
        Initialize();
    }

    private void Initialize()
    {
        deathUiCanvas.SetActive(false);
    }

    public void LaunchDeathUI()
    {
        deathUiCanvas.SetActive(true);
    }

    public void RestartClicked()
    {
        GameManager.Instance.RestartGame();
    }

    public void Reset()
    {
        Initialize();
    }
}
