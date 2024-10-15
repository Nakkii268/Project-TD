using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat 
{
    [SerializeField]private float BaseStat;
    [SerializeField] private float Modifier;

    public float GetFinalStat(float multiplier)
    {
        return BaseStat + Modifier * multiplier;
    }
    public float GetBaseStat()
    {
        return BaseStat;
    }
}
