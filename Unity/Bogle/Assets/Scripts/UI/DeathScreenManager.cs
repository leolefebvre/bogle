using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenManager : Singleton<DeathScreenManager>, IResetable
{
    public GameObject deathUiCanvas;
    public AnimationClip openingAnimation;
    public AnimationClip closeAnimation;

    private Animator _animator;
    public Animator animator
    {
        get
        {
            if(_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            return _animator;
        }
    }

	// Use this for initialization
	void Start () {
        Initialize();
    }

    private void Initialize()
    {
        deathUiCanvas.SetActive(false);
    }

    public void OpenScreen()
    {
        deathUiCanvas.SetActive(true);
        animator.SetTrigger("OpenScreenTrigger");
    }

    public void CloseScreen()
    {
        animator.SetTrigger("CloseScreenTrigger");
        GameManager.Instance.RestartGame();
    }

    public void RestartClicked()
    {
        CloseScreen();
    }

    public void Reset()
    {
        Initialize();
    }
}
