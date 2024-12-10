using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllianceHeathBar : MonoBehaviour
{
    [SerializeField] private Alliance alliance;
    [SerializeField] private Image hpBar;

    private void Start()
    {
        alliance.OnHpChange += Alliance_OnHpChange;
        hpBar.fillAmount = 1;
    }

    private void Alliance_OnHpChange(object sender, float e)
    {
        hpBar.fillAmount = e;
        
    }
}
