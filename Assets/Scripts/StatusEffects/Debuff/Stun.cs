using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffect/")]
public class Stun : OnHitStatusEffect
{
    public float Radius;
    public float Scale;
    public DamageType DamageType;

    public override void OnApply( GameObject target)
    {
        target.GetComponentInChildren<StatusEffectHolder>().GetDisableEffect();


    }
    public override void OnRemove(GameObject target)
    {
        target.GetComponentInChildren<StatusEffectHolder>().RemoveDisableEffect();

    }

}
