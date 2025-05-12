using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="LimitBreakIcon")]
public class LimitBreakIcon : ScriptableObject
{
    public List<LBIcon> IconList;

    public Sprite GetIcon(int index)
    {
        return IconList[index].Icon;
    }
}
[Serializable] 
public class LBIcon
{
    public int lbLevel;
    public Sprite Icon;
}
