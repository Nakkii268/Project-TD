using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "StatusEffect/AttackSpeedBuff")]

public class AttackSpeedBuff : NormalStatusEffect
{
    public StatModifier modifier;


    public override void OnApply(GameObject target)
    {
        target.GetComponentInParent<Alliance>().Stat.AttackInterval.AddingModifier(modifier);
    }
    public override void OnRemove(GameObject target)
    {
        target.GetComponentInParent<Alliance>().Stat.AttackInterval.RemovingModifier(modifier);
    }
}
