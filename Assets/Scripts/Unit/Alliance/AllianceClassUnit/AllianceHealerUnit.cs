using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Unit/Alliance/Healer")]
public class AllianceHealerUnit : AllianceUnit
{
    public HealerBranch unitBranch;
    public float HealScale;
    public override void ApplyClassBuff(GameObject unit)
    {
    }
}
