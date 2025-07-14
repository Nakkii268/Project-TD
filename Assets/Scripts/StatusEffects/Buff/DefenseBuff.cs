using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "StatusEffect/DefenseBuff")]

public class DefenseBuff : NormalStatusEffect
{
    public StatModifier modifier;


    public override void OnApply(GameObject target)
    {
        target.GetComponentInParent<Alliance>().Stat.Defense.AddingModifier(modifier);
    }
    public override void OnRemove(GameObject target)
    {
        target.GetComponentInParent<Alliance>().Stat.Defense.RemovingModifier(modifier);
    }
}
