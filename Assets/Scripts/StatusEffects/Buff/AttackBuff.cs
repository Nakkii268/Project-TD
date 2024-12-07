using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="StatusEffect/AttackBuff")]
public class AttackBuff : StatusEffect
{
    public float amout;
    public StatModType type;

    public override void OnApply(GameObject target)
    {
        target.TryGetComponent<Alliance>(out Alliance ally);
        ally.Stat.Attack.AddingModifier(new StatModifier(amout, type)); 
    }
}
