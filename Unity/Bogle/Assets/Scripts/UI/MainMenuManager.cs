using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : Singleton<MainMenuManager>
{
    public AudioSource MusicSource;

    public AudioClip GameMusic;

    // Use this for initialization
    void Start()
    {
        if (SceneManager.sceneCount > 1)
        {
            LaunchGame();
        }
        else
        {
            HealthDisplayManager.Instance.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.anyKeyDown)
        {
            LaunchGame();
        }
	}

    void LaunchGame()
    {
        MusicSource.clip = GameMusic;
        MusicSource.Play();

        GameManager.Instance.ReceivedRightToLaunchArenaMode();
        HealthDisplayManager.Instance.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }
}
