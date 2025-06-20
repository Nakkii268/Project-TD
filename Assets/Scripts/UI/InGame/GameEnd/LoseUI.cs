using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : UICanvas
{
    [SerializeField] private Button exitBtn;
    [SerializeField] private TextMeshProUGUI stageName;

    public override void SetUp(object t)
    {
        if (t is MapData map)
        {
            Initialized(map);
        }
    }
    private void Start()
    {
        exitBtn.onClick.AddListener(() =>
        {
            GameManager.Instance._sceneLoader.LoadMenu();
            
            UIManager.Instance.OpenUI<StageSelectUI>();
        });
        exitBtn.interactable = false;
        StartCoroutine(EnableExitBtn());
    }
    private void Initialized(MapData mapData)
    {
        stageName.text = mapData.Map.MapID;
       
    }

private IEnumerator EnableExitBtn()
    {
        yield return new WaitForSeconds(1);
        exitBtn.interactable = true;    
    } 
 }
