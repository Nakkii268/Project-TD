using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    [SerializeField] private int RecoveryTime; //time in second
    [SerializeField] private int _currentStamina;
    public int CurrentStamina { get { return _currentStamina; } }
    [SerializeField] private int MaxStamina;
    [SerializeField] private long LastLogin;
    [SerializeField] private int TimeSpan; //time left after cal, will add to recover
  
    private void Start()
    {
        GetData();
        StaminaCal();
        StartCoroutine(StaminaRecover(RecoveryTime));
    }
    private void GetData()
    {
        LastLogin = GameManager.Instance._playerDataManager.PlayerDataSO.LastLogin;
        _currentStamina = GameManager.Instance._playerDataManager.PlayerDataSO.Stamina;
    }
    private void StaminaCal()
    {
        DateTime current = DateTime.UtcNow;
        TimeSpan timeDiff = current - new DateTime(LastLogin);
        
        AddStamina( (int)timeDiff.TotalSeconds / RecoveryTime);
        TimeSpan = (int)timeDiff.TotalSeconds % RecoveryTime;
    }
    
    IEnumerator StaminaRecover(float RecoveryTime)
    {
        while (_currentStamina < MaxStamina)
        {
            yield return new WaitForSeconds(RecoveryTime - TimeSpan);
            AddStamina(1);
            TimeSpan = 0;
        }
    }
    private void AddStamina(int stamina)
    {
        
            _currentStamina += stamina;
            if (_currentStamina >= MaxStamina)
            {
                _currentStamina = MaxStamina;
            }
            GameManager.Instance._playerDataManager.PlayerDataSO.SaveStamina(_currentStamina);
            DateTime current = DateTime.UtcNow;
            GameManager.Instance._playerDataManager.PlayerDataSO.SaveLoginTime(current.Ticks); // save time whenever stamina recovery (temp solution/ in case on app quit not working)

    }
    public bool StaminaConsumed(int amount)
    {
        if (_currentStamina >= amount)
        {
            _currentStamina -= amount;
            GameManager.Instance._playerDataManager.PlayerDataSO.SaveStamina(_currentStamina);
            return true;
        }
        return false;
    }

    private void OnApplicationQuit()
    {
        GameManager.Instance._playerDataManager.PlayerDataSO.SaveStamina(_currentStamina);
        DateTime current = DateTime.UtcNow;
        GameManager.Instance._playerDataManager.PlayerDataSO.SaveLoginTime( current.Ticks);
        

    }
    public string GetStaminaTxt()
    {
        return _currentStamina.ToString()+"/"+MaxStamina.ToString();
    }
    
}
