using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private MapSO m_MapSO;
    [SerializeField] private int currentWaveIndex;
    [SerializeField] private int nextWaveIndex;
    [SerializeField] private float entryTime;
    [SerializeField] private int enemyCount;
    [SerializeField] private TextMeshProUGUI enemyCountText;

    private void Start()
    {
        currentWaveIndex = 0;
        nextWaveIndex = 0;
        entryTime = levelManager.LevelStateMachineManager.entryTime;
        m_MapSO = levelManager.Map;
        enemyCount = 0;
        SetEnemyCountText(0);
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
        Enemy enemyClass = enemy.GetComponent<Enemy>();
        enemyClass.SetPath(wave.Path);
        enemyClass.OnEnemyDead += EnemyClass_OnEnemyDead;
        enemy.transform.position = wave.SpawnPos;
        
    }

    private void EnemyClass_OnEnemyDead(object sender, EnemyDeadArg e)
    {
        if (e.isWave)
        {
            enemyCount++;
            SetEnemyCountText(enemyCount);
            if(enemyCount == m_MapSO.TotalEnemy)
            {
                levelManager.GameEnd();
            }
        }
    }
    private void SetEnemyCountText(int curr)
    {
        enemyCountText.text = curr.ToString() + " / " + m_MapSO.TotalEnemy.ToString();
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
