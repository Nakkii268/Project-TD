using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private MapSO m_MapSO;
    [SerializeField] private int currentWaveIndex;

    private void Start()
    {
        currentWaveIndex = 0;
    }

    //spawn wave when time come
    //after done spawn wave, current wave index increase
}
