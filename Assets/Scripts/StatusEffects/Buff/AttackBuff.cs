using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName ="StatusEffect/AttackBuff")]
public class AttackBuff : NormalStatusEffect
{
    
    public StatModifier modifier;

   
    public override void OnApply(GameObject target)
    {
        target.GetComponentInParent<Alliance>().Stat.Attack.AddingModifier(modifier); 
    }
    public override void OnRemove(GameObject target)
    {
        target.GetComponentInParent<Alliance>().Stat.Attack.RemovingModifier(modifier);
    }

    
}
