using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffect/OnHitEffect/AttackAllInRange")]
public class AttackAllTargetInRange : OnHitStatusEffect
{
    public override void OnApply(GameObject holder, GameObject target)
    {
        holder.GetComponent<Alliance>().AllianceAttack.SetTargetCount(TargetCount.AllInRange);
    }

}
