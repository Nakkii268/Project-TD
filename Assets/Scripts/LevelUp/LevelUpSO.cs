using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="LevelUpData")]
public class LevelUpSO : ScriptableObject
{
    public List<LevelUpData> data;
}
[Serializable]
public class LevelUpData
{
    public int LimitBreakLevel;
    public List<StatBonus> StatBonus;//per level
    public List<ItemsData> CurrencyRequired;//perlevel
}

[Serializable]

public class StatBonus
{
    public StatName StatName;
    public int StatModify;
}