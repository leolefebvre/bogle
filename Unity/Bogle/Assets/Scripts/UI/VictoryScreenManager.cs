using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenManager : Singleton<VictoryScreenManager>, IResetable
{
    public GameObject victoryUiCanvas;

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        victoryUiCanvas.SetActive(false);
    }

    public void LaunchVictoryUI()
    {
        victoryUiCanvas.SetActive(true);
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