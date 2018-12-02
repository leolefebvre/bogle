using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum ShakeTypes
{
    fireShake,
    playerTakesHitShake,
    ennemyDeathShake,
    noShake
}

[System.Serializable]
public struct ShakeParameters
{
    public ShakeTypes shakeType;
    public float ShakeDuration;          // Time the Camera Shake effect will last
    public float shakeAmplitude;         // Cinemachine Noise Profile Parameter
    public float shakeFrequency;         // Cinemachine Noise Profile Parameter
}



public class CameraShakeControler : Singleton<CameraShakeControler>
{
    public ShakeParameters defaultState;
    public List<ShakeParameters> shakelist;

    private float ShakeElapsedTime = 0f;
    private ShakeParameters currentShakeParameter;
    private bool isShaking = false;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;
    public CinemachineBasicMultiChannelPerlin virtualCameraNoise
    {
        get
        {
            if(_virtualCameraNoise == null)
            {
                _virtualCameraNoise = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
            return _virtualCameraNoise;
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            if (ShakeElapsedTime > 0f)
            {
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                StopShaking();
            }
        }
    }

    public void LaunchShake(ShakeTypes shakeType)
    {
        if(isShaking && currentShakeParameter.shakeType > shakeType)
        {
            return;
        }

        bool foundShakeParameter = false;

        foreach(ShakeParameters shakeParameter in shakelist)
        {
            if(shakeParameter.shakeType == shakeType)
            {
                currentShakeParameter = shakeParameter;
                foundShakeParameter = true;
            }
        }

        if(!foundShakeParameter)
        {
            return;
        }

        isShaking = true;

        ApplyShakeParameters(currentShakeParameter);
    }

    private void ApplyShakeParameters(ShakeParameters shakeParameter)
    {
        virtualCameraNoise.m_AmplitudeGain = shakeParameter.shakeAmplitude;
        virtualCameraNoise.m_FrequencyGain = shakeParameter.shakeFrequency;
        ShakeElapsedTime = shakeParameter.ShakeDuration;
    }

    private void StopShaking()
    {
        ApplyShakeParameters(defaultState);
        isShaking = false;
        currentShakeParameter = defaultState;
    }

}
