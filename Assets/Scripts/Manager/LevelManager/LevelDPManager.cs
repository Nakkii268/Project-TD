using System;
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
  
    
    [SerializeField] private int deploymentLimit;
    [SerializeField] private int currentDeployment;
    public event EventHandler<float> OnDpChange;
    public event EventHandler<bool> OnDpReachMax;
    public event EventHandler<int> OnDeploymentChange;
    private bool isEnable;
    
    public void Init()
    {
        levelManager = LevelManager.instance;
        CurrentDp = levelManager.Map.StartDeployPoint;
        DpRegenRate = levelManager.Map.DpRecoveryRate;
        currentDeployment = levelManager.Map.DeployLimit;
        OnDpChange?.Invoke(this, CurrentDp);
        OnDeploymentChange?.Invoke(this, currentDeployment);
        isEnable = true;
    }
    private void Update()
    {
        if (!isEnable) return;
        if (CurrentDp < MaxDp)
        {
            CurrentDp += DpRegenRate * Time.deltaTime;
            OnDpChange?.Invoke(this, CurrentDp);
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
            OnDpReachMax?.Invoke(this, true);
        }
        OnDpChange?.Invoke(this, CurrentDp);
    }
    public void ReduceDP(float amount)
    {
        CurrentDp -= amount;
        if (CurrentDp < 0)
        {
            CurrentDp = 0;
        }
        OnDpReachMax?.Invoke(this, false);

        OnDpChange?.Invoke(this, CurrentDp);

    }
    public void DeploymentSlotOccp()
    {
        if (currentDeployment >0)
        {
            currentDeployment--;
            OnDeploymentChange?.Invoke(this, currentDeployment);
           

        }
    }
    public void DeploymentSlotFree()
    {
        if (currentDeployment < deploymentLimit)
        {
            currentDeployment++;
            OnDeploymentChange?.Invoke(this, currentDeployment);


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
        float elapseTime = 0;
        
        while (elapseTime < time)
        {
            float dp = (amount / time) * Time.deltaTime;
            AddingDP(dp);
            elapseTime += Time.deltaTime;
            yield return null;
        }
    }
   
}
