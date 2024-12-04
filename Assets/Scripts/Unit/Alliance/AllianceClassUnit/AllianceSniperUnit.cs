using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Unit/Alliance/Sniper")]
public class AllianceSniperUnit : AllianceUnit
{
    public SniperBranch unitBranch;
    public override void ApplyClassBuff()
    {
        base.ApplyClassBuff();
    }
}
