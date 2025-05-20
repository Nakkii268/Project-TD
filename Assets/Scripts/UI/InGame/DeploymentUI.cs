using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeploymentUI : UICanvas
{
    
    [SerializeField] private TextMeshProUGUI dptext;
    [SerializeField] private TextMeshProUGUI dltext;
    [SerializeField] private Transform dpMaxUI;
    [SerializeField] private Image DPRegenBar;

    private void Start()
    {
        LevelManager.instance.GetLevelDPManager().OnDeploymentChange += DeploymentUI_OnDeploymentChange;
        LevelManager.instance.GetLevelDPManager().OnDpChange += DeploymentUI_OnDpChange;
        LevelManager.instance.GetLevelDPManager().OnDpReachMax += DeploymentUI_OnDpReachMax;
        dpMaxUI.gameObject.SetActive(false);

    }
   

    private void DeploymentUI_OnDpReachMax(object sender, bool e)
    {
        if (e)
        {
            dpMaxUI.gameObject.SetActive(true);

        }
        else
        {
            dpMaxUI.gameObject.SetActive(false);

        }

    }

    private void DeploymentUI_OnDpChange(object sender, float e)
    {
        dptext.text = Mathf.FloorToInt(e).ToString();
        
            DPRegenBar.fillAmount = e%1f;
        

    }

    private void DeploymentUI_OnDeploymentChange(object sender, int e)
    {
        dltext.text = "Deployment Limit: " + e.ToString();

    }
}
