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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize()
    {
        defaultSoftZoneParameters.x = cameraBody.m_SoftZoneWidth;
        defaultSoftZoneParameters.y = cameraBody.m_SoftZoneHeight;
    }

    public void RestartGame()
    {
        Debug.Log("Reload " + SceneManager.GetActiveScene().name);
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
}
