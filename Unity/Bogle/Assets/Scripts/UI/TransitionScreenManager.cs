using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreenManager : Singleton<TransitionScreenManager>, IResetable
{
    public GameObject transitionCanvas;

    public List<ChoiceUI> choicesUi;
    public Button confirmButton;

    public int currentChoice = -1;

	// Use this for initialization
	void Start ()
    {
        Initialise();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Initialise()
    {
        DisableConfirmButton();
        transitionCanvas.SetActive(false);

        foreach(ChoiceUI choice in choicesUi)
        {
            choice.Reset();
        }
    }

    public void DisableConfirmButton()
    {
        confirmButton.interactable = false;
        currentChoice = -1;
    }

    public void OpenTransitionUI()
    {
        transitionCanvas.SetActive(true);
    }

    public void OnChoiceClicked(int choiceIndex)
    {
        currentChoice = choiceIndex;
        confirmButton.interactable = true;
    }

    public void OnConfirmButtonClicked()
    {
        if(choicesUi[currentChoice].choiceAvailable == false)
        {
            return;
        }
        
        choicesUi[currentChoice].TickNextBox();

        StatsManager.Instance.ReduceStat(choicesUi[currentChoice].linkedStat);

        DisableConfirmButton();
        GameManager.Instance.LoadNextLevel();
        transitionCanvas.SetActive(false);
    }

    public void Reset()
    {
        Initialise();
    }
}
