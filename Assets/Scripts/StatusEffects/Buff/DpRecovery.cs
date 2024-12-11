using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="StatusEffect/DpRecover")]
public class DpRecovery : StatusEffect
{
    public int RecoverAmount;
    

    public override void OnApply(GameObject target)
    {
        LevelManager.instance.GetLevelDPManager().DPRecoverSlow(RecoverAmount, duration);
    }
}
