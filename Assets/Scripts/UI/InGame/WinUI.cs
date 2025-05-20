using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : UICanvas
{
    [SerializeField] private Button exitBtn;

    public override void SetUp(object t)
    {
        if ( t is bool comp) {
            Initialized(comp);
        }
    }
    private void Start()
    {
        exitBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseAllUI();
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

    private void Initialized(bool full)
    {
        //if true = fully complete
        //false = not fully complete
    }
}
