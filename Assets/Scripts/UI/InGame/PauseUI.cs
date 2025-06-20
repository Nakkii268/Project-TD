using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : UICanvas
{
    [SerializeField] private Button ExitBtn;
    [SerializeField] private Button ResumeBtn;

    public override void SetUp()
    {
        ExitBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            GameManager.Instance._sceneLoader.LoadMenu();
        });
        ResumeBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            UIManager.Instance.Close<PauseUI>(0);
        });
    }
}
