using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDPManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
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
        levelManager = LevelManager.instance;
        CurrentDp = levelManager.Map.StartDeployPoint;
        DpRegenRate = levelManager.Map.DpRecoveryRate;
        currentDeployment = levelManager.Map.DeployLimit ;
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
    public void AddingDP(float amount)
    {
        if (CurrentDp >= MaxDp) return;
        CurrentDp += amount;
        if (CurrentDp > MaxDp)
        {
            CurrentDp = MaxDp;
        }
    }
    public void ReduceDP(float amount)
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

    public void DPRecoverSlow(int amount,float time)
    {
        StartCoroutine(DPRecover(amount,time));
    }
    private IEnumerator DPRecover(int amount,float time)
    {
        float timePerDP = time / amount;
        int recovered = 0;
        while (recovered < amount)
        {
            AddingDP(1);
            recovered++;
            yield return new WaitForSeconds(timePerDP);
        }
    }
   
}
