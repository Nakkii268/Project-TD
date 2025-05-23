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
    public PlayerData Data;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
       
  
    }
    private void Start()
    {
        StartCoroutine(delay());
    }

    public void TestBtn()
    {
        
      
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(10);
        SaveLoadData.ConvertToSO(testData, Data);

    }
}
