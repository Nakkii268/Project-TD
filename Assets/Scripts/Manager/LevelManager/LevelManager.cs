using System;
using System.Collections;
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
    
    [SerializeField] private int currentUnitIndex;
    
    

    [SerializeField] private LevelLifePointManager levelLifePointManager;
    [SerializeField] public LevelLifePointManager LevelLifePointManager {  get { return levelLifePointManager; } }
    [SerializeField] private WaveManager waveManager;
    [SerializeField] public WaveManager WaveManager{  get { return waveManager; } }
    [SerializeField] private ParticleManager _particleManager;
    public ParticleManager ParticleManager { get { return _particleManager; } }
    [SerializeField] private LevelStateMachineManager levelStateMachineManager;
    [SerializeField] private Transform LevelInfoUI;
    public LevelStateMachineManager LevelStateMachineManager { get { return levelStateMachineManager; } }

    [SerializeField] private ObjectPoolManager _poolManager;
    public ObjectPoolManager projectilePool { get { return _poolManager; } }


    public event EventHandler<EndState> OnGameEnd;// win = 1; lose = 0; win but leaked =0.x


    private void Awake()
    {
        instance = this;
        
        levelStateMachineManager = new LevelStateMachineManager(this);

     
       
       
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

   
    public void EnableComponent()
    {
        levelDPManager.Init();
        waveManager.Init();
        levelLifePointManager.Init();
    }

    public void TimeSlow()
    {
    
        Time.timeScale = .1f;
    }
    public void TimeNormal()
    {
     
        Time.timeScale = 1f;
    }
    public void DelayTime(float delay)
    {
        StartCoroutine(Delay(delay));
    }
   public IEnumerator Delay(float time)
   {
        yield return new WaitForSeconds(time);
   }
   
}

