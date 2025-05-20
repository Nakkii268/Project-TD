using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelLifePointManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private int LifePoint;
    [SerializeField] private int MaxLifePoint;

    [SerializeField] private int ReducedLifePoint;
    public event EventHandler<LifePointEvtArg> OnLifePointChange;
 
    
    public void Init()
    {
        levelManager = LevelManager.instance;
        MaxLifePoint = levelManager.Map.LifePoint;
        ReducedLifePoint = 0;
        LifePoint = MaxLifePoint;
        OnLifePointChange?.Invoke(this, new LifePointEvtArg(LifePoint, ReducedLifePoint));
    }
    public void LifePointReduce()
    {
        LifePoint--;
        ReducedLifePoint++;
        OnLifePointChange?.Invoke(this, new LifePointEvtArg(LifePoint, ReducedLifePoint));

      
        if (LifePoint <= 0) {
            levelManager.GameEnd();
        }

    }
    public EndState GetGameEndState()
    {
        if ((LifePoint / MaxLifePoint)==1)
        {
            
            return EndState.Successed;

        }
        else if((LifePoint / MaxLifePoint) == 0)
        {
           
            return EndState.Failed;


        }
        else
        {
            

            return EndState.NotComplete;

        }


    }
   
}
[Serializable]
public enum EndState
{
    Successed,
    Failed,
    NotComplete
}
public class LifePointEvtArg
{
    public int Remain;
    public int Reduced;
   

    public LifePointEvtArg(int lifePoint, int reducedLifePoint)
    {
        this.Remain = lifePoint;
        this.Reduced = reducedLifePoint;
    }
}