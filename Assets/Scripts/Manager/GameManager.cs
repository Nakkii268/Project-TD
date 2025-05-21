using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public SceneLoader _sceneLoader;
    [SerializeField] public LimitBreakIcon limitBreakIcon;
    public string stagePath;
    public PlayerData testData;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void TestBtn()
    {
        _sceneLoader.LoadStage(stagePath);
    }
}
