using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDPManager : MonoBehaviour
{
    [SerializeField] private float CurrentDp;
    [SerializeField] private int StartDp;
    [SerializeField] private int MaxDp;
    [SerializeField] private float DpRegenRate;
    [SerializeField] private TextMeshProUGUI dptext;
    [SerializeField] private TextMeshProUGUI dltext;
    [SerializeField] private Transform dpMaxUI;
    [SerializeField] private int deploymentLimit;
    [SerializeField] private int currentDeployment;

    private void Start()
    {
        CurrentDp = StartDp;
        currentDeployment = deploymentLimit;
        dptext.text = Mathf.FloorToInt(CurrentDp).ToString();
        dltext.text = "Deployment Limit: "+ currentDeployment.ToString();

    }

    private void Update()
    {
        if (CurrentDp < MaxDp) {
             CurrentDp +=  DpRegenRate * Time.deltaTime;
            dptext.text = Mathf.FloorToInt(CurrentDp).ToString();
            dpMaxUI.gameObject.SetActive(false);

        }
        else
        {
            dpMaxUI.gameObject.SetActive(true);
        }
    }
    public float GetCurrentDp()
    {
        return CurrentDp;
    }
    public void AddingDP(int amount)
    {
        if (CurrentDp >= MaxDp) return;
        CurrentDp += amount;
        if (CurrentDp > MaxDp)
        {
            CurrentDp = MaxDp;
        }
    }
    public void ReduceDP(int amount)
    {
        CurrentDp -= amount;
        if (CurrentDp < 0)
        {
            CurrentDp = 0;
        }
    }
    public void DeploymentSlotOccp()
    {
        if (currentDeployment >0)
        {
            currentDeployment--;
            dltext.text = "Deployment Limit: " + currentDeployment.ToString();

        }
    }
    public void DeploymentSlotFree()
    {
        if (currentDeployment < deploymentLimit)
        {
            currentDeployment++;
            dltext.text = "Deployment Limit: " + currentDeployment.ToString();

        }
    }
    public bool ReachDeployLimit()
    {
        return currentDeployment > 0;
    }

}
