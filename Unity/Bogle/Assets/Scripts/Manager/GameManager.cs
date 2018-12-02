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
    public GameState currentGameState = GameState.menu;
    public CinemachineVirtualCamera cinemachineCamera;
    public float cameraFocusTime = 0.1f;

    public List<string> levelNameOrder;

    private Vector2 defaultSoftZoneParameters;

    public int baseEnnemyCount = 0;
    public int currentEnnemyCount = 0;

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
        Debug.Log("Initlalize Level");

        CountNumberOfEnnemies();
    }

    public void CountNumberOfEnnemies()
    {
        baseEnnemyCount = FindObjectsOfType<BaseEnemy>().Count();
    }
}
