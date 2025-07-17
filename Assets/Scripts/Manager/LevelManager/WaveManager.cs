using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private PreWavePath wavePath;
    [SerializeField] private MapSO m_MapSO;
    [SerializeField] private int currentWaveIndex;
    [SerializeField] private int nextWaveIndex;
    [SerializeField] private float entryTime;
    [SerializeField] private int enemyCount;
    private bool isEnable;
    public event EventHandler<WaveEvtarg> OnEnemyChange;

    public void Init()
    {
        currentWaveIndex = 0;
        nextWaveIndex = 0;
        entryTime = levelManager.LevelStateMachineManager.entryTime;
        m_MapSO = levelManager.Map;
        enemyCount = 0;
        OnEnemyChange?.Invoke(this, new WaveEvtarg(enemyCount, m_MapSO.TotalEnemy));
        isEnable = true;
        for(int i = 0;i < m_MapSO.Waves.Length; i++)
        {
            StartCoroutine(DelaySpawn(m_MapSO.Waves[i]));
        }
    }
    private void Update()
    {
        if (!isEnable) return;   
        if (nextWaveIndex >= m_MapSO.Waves.Length) return;

        //visualize enemy path
        if (Time.time - entryTime >= m_MapSO.Waves[nextWaveIndex].SpawnTime- 3f)
        {
            if (!wavePath.isPathVisualize)
            {
                wavePath.isPathVisualize = true;
                StartCoroutine(wavePath.VisualizePath(m_MapSO.Waves[currentWaveIndex].Path));
            }
            


        }
        //spawn
       // for (int i = 0; i < m_MapSO.Waves.Length; i++)
       // {
            
       //     if (Time.time - entryTime >= m_MapSO.Waves[i].SpawnTime)
        //    {

         //       StartCoroutine(DelaySpawn(m_MapSO.Waves[i]));
         //       nextWaveIndex++;

         //   }
        //}
    }
    
    private void SpawnWave(Wave wave)
    {
        GameObject enemy = Instantiate(wave.EnemyUnit.UnitPrefab);
        Enemy enemyClass = enemy.GetComponent<Enemy>();
        enemyClass.SetPath(wave.Path);
        enemyClass.SetUnit(wave.EnemyUnit);
        enemyClass.isWaveEnemy = true;
        enemyClass.OnEnemyDead += EnemyClass_OnEnemyDead;
        enemy.transform.position = wave.SpawnPos;
        
    }

    private void EnemyClass_OnEnemyDead(object sender, EnemyDeadArg e)
    {
        if (e.isWave)
        {
            enemyCount++;
            OnEnemyChange?.Invoke(this, new WaveEvtarg(enemyCount, m_MapSO.TotalEnemy));

            if (enemyCount == m_MapSO.TotalEnemy)
            {
                levelManager.GameEnd();
            }
        }
    }
  

    private IEnumerator DelaySpawn( Wave wave)
    {
        yield return new WaitForSeconds(wave.SpawnTime);
        for (int i = 0; i < wave.EnemyQuantity; i++)
        {
            SpawnWave(wave);
           
            yield return new WaitForSeconds(wave.SpawnDelay);
        }
        
        currentWaveIndex++;
    }
    private IEnumerator VisualizePath(int index)
    {

    }
}
public class WaveEvtarg
{
    public int EnemyCount;
    public int MaxEnemyCount;
    public WaveEvtarg(int enemyCount, int maxEnemyCount)
    {
        EnemyCount = enemyCount;
        MaxEnemyCount = maxEnemyCount;
    }
}