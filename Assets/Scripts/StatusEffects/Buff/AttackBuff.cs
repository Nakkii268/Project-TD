using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName ="StatusEffect/AttackBuff")]
public class AttackBuff : NormalStatusEffect
{
    
    public StatModifier modifier;

    public void Init(float duration,bool stack,StatusType st, float amout, StatModType type) // for factory
    { 
        this.duration = duration;
        this.Stackable = stack;
        this.SType = st;
        modifier = new StatModifier(amout, type);
    }
    
    public override void OnApply(GameObject target)
    {
        target.TryGetComponent<Alliance>(out Alliance ally);
        ally.Stat.Attack.AddingModifier(modifier); 
    }
    public override void OnRemove(GameObject target)
    {
        target.TryGetComponent<Alliance>(out Alliance ally);
        ally.Stat.Attack.RemovingModifier(modifier);
    }

    
}
