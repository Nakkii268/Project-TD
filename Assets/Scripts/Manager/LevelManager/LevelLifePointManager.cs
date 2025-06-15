using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelLifePointManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private float LifePoint;
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
        else if(0< (LifePoint / MaxLifePoint) && (LifePoint / MaxLifePoint) <1)
        {
           
            return EndState.NotComplete;


        }
        else
        {
            

            return EndState.Failed;

        }


    }
   
}
[Serializable]
public enum EndState
{
    NotComplete,
    Failed,
    Successed
}
public class LifePointEvtArg
{
    public float Remain;
    public float Reduced;
   

    public LifePointEvtArg(float lifePoint, float reducedLifePoint)
    {
        this.Remain = lifePoint;
        this.Reduced = reducedLifePoint;
    }
}