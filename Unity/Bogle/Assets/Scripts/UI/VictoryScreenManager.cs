using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenManager : Singleton<VictoryScreenManager>, IResetable
{
    public GameObject victoryUiCanvas;
    public AnimationClip closeAnimation;

    private Animator _animator;
    public Animator animator
    {
        get
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            return _animator;
        }
    }


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
        GameManager.Instance.RestartGame(closeAnimation.length);
    }

    public void Reset()
    {
        Initialize();
    }
}