using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private MapSO map;
    public MapSO Map { get  { return map; } }
    [SerializeField] private MapManager mapManager;
    public MapManager MapManager { get {    return mapManager; } }
    [SerializeField] private InGameCharListUI charListUI;
    [SerializeField] private LevelDPManager levelDPManager;
    [SerializeField] private int currentUnitIndex;
    [SerializeField] private int deloymentLimit;
    [SerializeField] private int currentDeloyment = 0;

    [SerializeField] private LevelLifePointManager levelLifePointManager;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private LevelStateMachineManager levelStateMachineManager;
    [SerializeField] private Transform LevelInfoUI;
    public LevelStateMachineManager LevelStateMachineManager { get { return levelStateMachineManager; } }


   
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnPause;
    public event EventHandler<float> OnGameEnd;// win = 1; lose = 0; win but leaked =0.x



    private void Awake()
    {
        instance = this;
        
        levelStateMachineManager = new LevelStateMachineManager(this);
        DisableComponent();
    }
    private void Start()
    {
        //
        
        levelStateMachineManager.ChangeState(levelStateMachineManager.LevelPrepareState);
    }
    //
    private void Update()
    {
       
        levelStateMachineManager.Update();
        
    }
   
   
    public LevelDPManager GetLevelDPManager()
    {
        return levelDPManager;
    }
   
    public void GameEnd()
    {
        OnGameEnd?.Invoke(this, levelLifePointManager.GetGameEndLifePoint());
    }

    public void DisableComponent()
    {
        levelDPManager.gameObject.SetActive(false);
        waveManager.gameObject.SetActive(false);
        LevelInfoUI.gameObject.SetActive(false );
    }
    public void EnableComponent()
    {
        levelDPManager.gameObject.SetActive(true);
        waveManager.gameObject.SetActive(true);
        LevelInfoUI.gameObject.SetActive(true );
    }

    
}

