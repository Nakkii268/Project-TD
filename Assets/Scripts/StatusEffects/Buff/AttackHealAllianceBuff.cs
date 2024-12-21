using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="StatusEffect/HealerBuff")]
public class AttackHealAllianceBuff : StatusEffect
{
    public override void OnApply(GameObject target)
    {
        target.TryGetComponent<Alliance>(out Alliance ally);
        ally.GetAllianceUnit().UnitTarget = UnitTarget.Alliance;
      
    }
    public override void OnRemove(GameObject target)
    {
        target.TryGetComponent<Alliance>(out Alliance ally);
        ally.GetAllianceUnit().UnitTarget = UnitTarget.Enemy;
    }
}
