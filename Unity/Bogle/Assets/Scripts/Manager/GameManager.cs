using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public enum GameState
{
    arena,
    menu
}

public class GameManager : Singleton<GameManager>
{
    [Header("Level Order Parameter")]
    public List<string> levelNameOrder;

    [Header("Camera Reset parameters")]
    public CinemachineVirtualCamera cinemachineCamera;
    private Vector2 defaultSoftZoneParameters;
    public float cameraFocusTime = 0.1f;

    [Header("Character Reset parameter")]
    public float defaultYPosition = 0f;
    public Vector3 defaultRotation;    

    [Space(20f)]
    [Header("Internal logic Parameter DON'T TOUCH")]

    public GameState currentGameState = GameState.arena;
    public int baseEnemyCount = 0;
    public int currentEnemyCount = 0;

    public int currentLevel = 0;

    private CinemachineFramingTransposer _cameraBody;
    public CinemachineFramingTransposer cameraBody
    {
        get
        {
            if(_cameraBody == null)
            {
                _cameraBody = cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
            return _cameraBody;
        }
    }

    // Use this for initialization
    void Start ()
    {
        Initialize();

        if(SceneManager.sceneCount > 1)
        {
            InitializeLevel();
        }
        else
        {
            RestartGame();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;

        defaultSoftZoneParameters.x = cameraBody.m_SoftZoneWidth;
        defaultSoftZoneParameters.y = cameraBody.m_SoftZoneHeight;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(levelNameOrder[0]);
        currentLevel = 0;

        ResetCommonScene();
    }

    public void ResetCommonScene()
    {
        CrabControler.Instance.Reset();
        
        var allResetables = FindObjectsOfType<MonoBehaviour>().OfType<IResetable>();
        foreach (IResetable resetable in allResetables)
        {
            resetable.Reset();
        }
    }

    public void FocusCameraOnCharacter()
    {
        cameraBody.m_SoftZoneWidth = 0f;
        cameraBody.m_SoftZoneHeight = 0f;
    }

    IEnumerator GiveBackCameraFreedom()
    {
        yield return new WaitForSeconds(cameraFocusTime);

        cameraBody.m_SoftZoneWidth = defaultSoftZoneParameters.x;
        cameraBody.m_SoftZoneHeight = defaultSoftZoneParameters.y;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        CountNumberOfEnnemies();
        DeathScreenManager.Instance.Reset();

        Vector3 spawnPosition = GetSpawnerPosition();
        spawnPosition.y = defaultYPosition;

        CrabControler.Instance.transform.position = spawnPosition;
        CrabControler.Instance.transform.eulerAngles = defaultRotation;

        SetGameState(GameState.arena);
    }

    private Vector3 GetSpawnerPosition()
    {
        Spawner spawner = FindObjectOfType<Spawner>();

        if(spawner == null)
        {
            Debug.Log("Can't find spawner");

            return Vector3.zero;
        }

        return spawner.transform.position;
    }

    public void CountNumberOfEnnemies()
    {
        baseEnemyCount = FindObjectsOfType<BaseEnemy>().Count();
        currentEnemyCount = baseEnemyCount;
    }

    public void RegisterDeadEnemy()
    {
        currentEnemyCount--;

        if(currentEnemyCount <= 0 && !CrabControler.Instance.isDead)
        {
            WinLevel();
        }
    }

    public void ManageCharacterDeath()
    {
        SetGameState(GameState.menu);

        DeathScreenManager.Instance.OpenScreen();
    }

    public void WinLevel()
    {
        SetGameState(GameState.menu);

        if(currentLevel == levelNameOrder.Count - 1)
        {
            VictoryScreenManager.Instance.LaunchVictoryUI();
        }
        else
        {
            TransitionScreenManager.Instance.OpenTransitionUI();
        }
    }

    public void LoadNextLevel()
    {
        currentLevel++;

        SceneManager.LoadScene(levelNameOrder[currentLevel]);
    }

    public void SetGameState(GameState newState)
    {
        if(currentGameState == newState)
        {
            return;
        }

        if(newState == GameState.arena)
        {
            // launch timer here
        }
        if(newState == GameState.menu)
        {
            StopAllEnnemy();
        }

        currentGameState = newState;
    }

    public void StopAllEnnemy()
    {
        BaseEnemy[] enemies = FindObjectsOfType<BaseEnemy>();

        foreach(BaseEnemy enemy in enemies)
        {
            enemy.Stop();
        }
    }
}
