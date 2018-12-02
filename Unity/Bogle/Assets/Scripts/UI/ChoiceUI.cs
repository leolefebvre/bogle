using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour, IResetable
{
    public List<Image> tickedBoxes;
    private int numberOfBoxesTicked = 0;

    public Stats linkedStat;

    private Button _linkedButton;
    public Button linkedButton
    {
        get
        {
            if(_linkedButton == null)
            {
                _linkedButton = GetComponent<Button>();
            }
            return _linkedButton;
        }
    }

    public bool choiceAvailable
    {
        get { return linkedButton.interactable; }
    }

    // Use this for initialization
    void Start ()
    {
        Reset();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Reset()
    {
        foreach(Image image in tickedBoxes)
        {
            image.enabled = false;
        }
        numberOfBoxesTicked = 0;
        linkedButton.interactable = true;
    }

    public void TickNextBox()
    {
        if(!choiceAvailable)
        {
            return;
        }

        tickedBoxes[numberOfBoxesTicked].enabled = true;
        numberOfBoxesTicked++;

        if(numberOfBoxesTicked == tickedBoxes.Count)
        {
            linkedButton.interactable = false;
        }
    }
}
