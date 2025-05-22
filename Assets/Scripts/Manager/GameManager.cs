using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public SceneLoader _sceneLoader;
    [SerializeField] public LimitBreakIcon limitBreakIcon;
    [SerializeField] public ResourceManager _resourceManager;
    public string stagePath;
    public PlayerDataSO testData;
    public Item test;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void TestBtn()
    {
        test = _resourceManager.GetItemById<Item>("G01");
      
    }
}
