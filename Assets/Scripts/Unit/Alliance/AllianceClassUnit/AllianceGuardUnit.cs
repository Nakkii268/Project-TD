using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Unit/Alliance/Guard")]
public class AllianceGuardUnit : AllianceUnit
{
    public GuardBranch unitBranch;
    public override void ApplyClassBuff()
    {
        base.ApplyClassBuff();
    }
}
