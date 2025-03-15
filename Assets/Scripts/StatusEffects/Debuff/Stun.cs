using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffect/Stun")]
public class Stun : NormalStatusEffect
{

    public override void OnApply( GameObject target)
    {
        target.GetComponentInParent<StatusEffectHolder>().GetDisableEffect();


    }
    public override void OnRemove(GameObject target)
    {
        target.GetComponentInParent<StatusEffectHolder>().RemoveDisableEffect();

    }

}
