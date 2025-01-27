using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private MapSO m_MapSO;
    [SerializeField] private int currentWaveIndex;
    [SerializeField] private float entryTime;

    private void Start()
    {
        currentWaveIndex = 0;
        entryTime = levelManager.LevelStateMachineManager.entryTime;
    }
    private void Update()
    {
        if(Time.time - entryTime < m_MapSO.Waves[currentWaveIndex].SpawnTime)
        {
            //spawn
            currentWaveIndex++;
        }   
    }
    //spawn wave when time come
    //after done spawn wave, current wave index increase
}
