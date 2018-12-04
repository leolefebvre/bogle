using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : Singleton<MainMenuManager>
{
    public AudioSource MusicSource;

    public AudioClip GameMusic;

    public GameObject Canvas;

    // Use this for initialization
    void Start()
    {
        Canvas.SetActive(true);
        if (SceneManager.sceneCount > 1)
        {
            LaunchGame();
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
