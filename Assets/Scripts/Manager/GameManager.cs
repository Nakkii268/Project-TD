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
    public Item item;

    public PointerClickHandle PointerClickHandler;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
       
  
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            TestBtn();
        }
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
        _playerDataManager.PlayerDataSO.AddItem(item, 10000);
        _playerDataManager.SaveItem();
        
    }

}
