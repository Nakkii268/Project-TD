using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffect/Debuff/Stun")]

public class Slow : NormalStatusEffect
{
    public StatModifier modifier;

    public override void OnApply(GameObject target)
    {
        target.GetComponentInParent<Enemy>().Stat.Speed.AddingModifier(modifier);
    }
    public override void OnRemove(GameObject target)
    {
        target.GetComponentInParent<Enemy>().Stat.Speed.RemovingModifier(modifier);

    }
}
