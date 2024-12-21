using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="StatusEffect/AOEattack")]
public class SpashAttackBuff : OnHitStatusEffect    
{
    public float Radius;
    public float Scale;

    public override void OnApply(GameObject holder, GameObject target)
    {
        base.OnApply(holder, target);
    }

}
