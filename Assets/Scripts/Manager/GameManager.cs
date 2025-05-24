using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public SceneLoader _sceneLoader;
    [SerializeField] public LimitBreakIcon limitBreakIcon;
    [SerializeField] public ResourceManager _resourceManager;
    [SerializeField] public PlayerDataManager _playerDataManager;
    public string stagePath;
    public PlayerDataSO testData;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
       
  
    }
    private void Update()
    {
       /* if (Input.GetKeyUp(KeyCode.Space))
        {
            TestBtn();
        }*/
    }
    private void Start()
    {
        _resourceManager.OnLoadComplete += _resourceManager_OnLoadComplete;
    }

    private void _resourceManager_OnLoadComplete(object sender, System.EventArgs e)
    {
        _sceneLoader.LoadMenu();
    }

    public void TestBtn()
    {
        PlayerDataSO newdata = ScriptableObject.CreateInstance<PlayerDataSO>();
        Debug.Log(newdata.PlayerName);
        newdata.PlayerName = "nanidesuka";
        Debug.Log(newdata.PlayerName);
      
    }

}
