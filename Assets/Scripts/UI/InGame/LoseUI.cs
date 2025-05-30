using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : UICanvas
{
    [SerializeField] private Button exitBtn;

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

    private IEnumerator EnableExitBtn()
    {
        yield return new WaitForSeconds(1);
        exitBtn.interactable = true;    
    } 
 }
