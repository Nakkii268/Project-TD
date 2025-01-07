using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

[CreateAssetMenu(menuName = "StatusEffect/AttackAllInRange")]
public class AttackAllTargetInRange : NormalStatusEffect
{
    public override void OnApply(GameObject target)
    {
        target.GetComponent<Alliance>().AllianceAttack.SetTargetCount(TargetCount.AllInRange);
    }

    public override void OnRemove(GameObject target)
    {
        target.GetComponent<Alliance>().AllianceAttack.SetTargetCount(TargetCount.Single);

    }

}
