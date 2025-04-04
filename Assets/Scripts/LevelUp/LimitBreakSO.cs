using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="LimitBreakData")]
public class LimitBreakSO : ScriptableObject
{
    public List<LimitBreakData> data;   
}
[Serializable]
public class LimitBreakData
{
    public List<Item> ItemsRequired;
    public List<StatusEffect> BuffGain;
    public int GoldRequired;
}
