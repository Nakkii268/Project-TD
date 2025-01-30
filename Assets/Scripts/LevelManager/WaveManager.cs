using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private MapSO m_MapSO;
    [SerializeField] private int currentWaveIndex;
    [SerializeField] private int nextWaveIndex;
    [SerializeField] private float entryTime;

    private void Start()
    {
        currentWaveIndex = 0;
        nextWaveIndex = 0;
        entryTime = levelManager.LevelStateMachineManager.entryTime;
        m_MapSO = levelManager.Map;
    }
    private void Update()
    {
        if (nextWaveIndex >= m_MapSO.Waves.Length) return;

        if (Time.time - entryTime >= m_MapSO.Waves[nextWaveIndex].SpawnTime)
        {
            Debug.Log("wave" + currentWaveIndex);
            Debug.Log("Time" + (Time.time - entryTime));

            StartCoroutine(DelaySpawn(m_MapSO.Waves[currentWaveIndex]));
            nextWaveIndex++;



        }
    }
    
    private void SpawnWave(Wave wave)
    {
        GameObject enemy = Instantiate(wave.EnemyUnit.UnitPrefab);
        enemy.GetComponent<Enemy>().SetPath(wave.Path);
        enemy.transform.position = wave.SpawnPos;
    }

    private IEnumerator DelaySpawn( Wave wave)
    {
        for (int i = 0; i < wave.EnemyQuantity; i++)
        {
            SpawnWave(wave);
           
            yield return new WaitForSeconds(wave.SpawnDelay);
        }
        Debug.Log("wave done");
        currentWaveIndex++;
    }
}
