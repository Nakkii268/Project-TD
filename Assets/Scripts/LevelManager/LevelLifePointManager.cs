using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLifePointManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private int LifePoint;
    void Start()
    {
        levelManager = LevelManager.instance;
        LifePoint = 1;// get from map SO through levelmanager
    }

    public void LifePointReduce()
    {
        LifePoint--;
        if (LifePoint <= 0) { 
            //lose
        }

    }
   
}
