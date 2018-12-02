using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stats
{
    speed,
    fireRate,
    range
}

public class StatsManager : Singleton<StatsManager>, IResetable
{
    [Tooltip("WalkingSpeed, rotationSpeed")]
    public List<Vector2> speedStats;
    public List<float> rangeStats;
    public List<float> fireRateStat;

    public int currentSpeedIndex = 0;
    public int currentRangeIndex = 0;
    public int currentFireRateIndex = 0;




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void Initialise()
    {
        FetchCharacterDefaultStats();
        currentSpeedIndex = 0;
        currentRangeIndex = 0;
        currentFireRateIndex = 0;
}

    private void FetchCharacterDefaultStats()
    {
        speedStats[0] = new Vector2(CrabControler.Instance.baseWalkingSpeed, CrabControler.Instance.baseRotatingSpeed);
        rangeStats[0] = CrabControler.Instance.baseRange;
        fireRateStat[0] = CrabControler.Instance.baseFireRate;
    }

    public void Reset()
    {
        Initialise();
    }

    public void ReduceStat(Stats statToReduce)
    {
        if(statToReduce == Stats.speed)
        {
            currentSpeedIndex++;
        }
        else if(statToReduce == Stats.fireRate)
        {
            currentFireRateIndex++;
        }
        else
        {
            currentRangeIndex++;
        }
        ApplyStatsToCharacter();
    }

    private void ApplyStatsToCharacter()
    {
        CrabControler.Instance.currentFireRate = fireRateStat[currentFireRateIndex];
        CrabControler.Instance.currentRange = rangeStats[currentRangeIndex];
        CrabControler.Instance.currentWalkingSpeed = speedStats[currentSpeedIndex].x;
        CrabControler.Instance.currentRotatingSpeed = speedStats[currentSpeedIndex].y;
    }
}
