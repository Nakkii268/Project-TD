using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Unit/Alliance/Vanguard")]
public class AllianceVanguardUnit : AllianceUnit
{
    public VanguardBranch unitBranch;
    public override void ApplyClassBuff()
    {
        base.ApplyClassBuff();
    }
}
