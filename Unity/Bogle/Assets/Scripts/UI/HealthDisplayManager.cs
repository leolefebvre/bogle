using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayManager : Singleton<HealthDisplayManager>, IResetable
{
    public List<Image> healthImagesRefs;

    public Sprite fullHeart;

    private int lastHeartLost = 0;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RemoveOneHeart()
    {
        if(lastHeartLost > healthImagesRefs.Count - 1)
        {
            return;
        }

        healthImagesRefs[lastHeartLost].enabled = false;
        lastHeartLost++;
    }

    public void Reset()
    {
        foreach(Image heartImage in healthImagesRefs)
        {
            heartImage.enabled = true;
            heartImage.sprite = fullHeart;
        }

        lastHeartLost = 0;
    }
}
