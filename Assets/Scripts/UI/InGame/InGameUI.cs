using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : UICanvas
{
    [SerializeField] private Button PauseBtn;

    public override void SetUp()
    {
        PauseBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 0;
            UIManager.Instance.OpenUI<PauseUI>();
        });
    }
}
