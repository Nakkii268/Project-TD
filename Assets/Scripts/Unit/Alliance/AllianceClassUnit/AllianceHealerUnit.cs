using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Unit/Alliance/Healer")]
public class AllianceHealerUnit : AllianceUnit
{
    public HealerBranch unitBranch;
    public override void ApplyClassBuff()
    {
        base.ApplyClassBuff();
    }
}
