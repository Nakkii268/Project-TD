using UnityEngine;

[CreateAssetMenu(menuName ="StatusEffect/AttackBuff")]
public class AttackBuff : StatusEffect
{
    
    public StatModifier modifier;

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
