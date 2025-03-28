using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] public SceneLoader _sceneLoader;
    public string stagePath;

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
