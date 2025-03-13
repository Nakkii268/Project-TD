using System;
using UnityEngine;



public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private MapSO map;
    public MapSO Map { get  { return map; } }
    [SerializeField] private MapManager mapManager;
    public MapManager MapManager { get {    return mapManager; } }
    [SerializeField] private InGameCharListUI charListUI;
    [SerializeField] private LevelDPManager levelDPManager;
    [SerializeField] private LevelEndUI endUI;
    public LevelEndUI EndUI { get { return endUI; } }
    [SerializeField] private int currentUnitIndex;
    [SerializeField] private int deloymentLimit;
    [SerializeField] private int currentDeloyment = 0;

    [SerializeField] private LevelLifePointManager levelLifePointManager;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private LevelStateMachineManager levelStateMachineManager;
    [SerializeField] private Transform LevelInfoUI;
    public LevelStateMachineManager LevelStateMachineManager { get { return levelStateMachineManager; } }

    [SerializeField] private Pooling _projectilePool;
    public Pooling projectilePool { get { return _projectilePool; } }
   
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnPause;
    public event EventHandler<EndState> OnGameEnd;// win = 1; lose = 0; win but leaked =0.x

    public PointerClickHandle PointerClickHandler;

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
        OnGameEnd?.Invoke(this, levelLifePointManager.GetGameEndState());
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

