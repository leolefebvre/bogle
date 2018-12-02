using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayManager : Singleton<HealthDisplayManager>
{
    public List<Image> healthImagesRefs;
    
    public Sprite emptyHeartSprite;

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

        healthImagesRefs[lastHeartLost].sprite = emptyHeartSprite;
        lastHeartLost++;
    }
}
