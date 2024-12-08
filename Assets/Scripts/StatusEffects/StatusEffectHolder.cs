using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StatusEffectHolder : MonoBehaviour
{
    public List<StatusEffect> effects;
    

    //add-remove
    public void AddStatusEffect(GameObject target,StatusEffect effect)
    {
        //make sure status effect not stack
        if (!effects.Contains(effect)) 
        {
            effects.Add(effect);
            StartEffectCoroutine(target, effect);
        }
        else
        {
            StopEffectCoroutine(target, effect);
            StartEffectCoroutine(target, effect);

        }
    }

    public void RemoveStatusEffect(GameObject target, StatusEffect effect)
    {
        if (!effects.Contains(effect)) return;
        effects.Remove(effect);
    }

    //start- stop
    public void StartEffectCoroutine(GameObject target, StatusEffect effect)
    {
        StartCoroutine(StatusEffectHandler(target, effect));
    }
    public void StopEffectCoroutine(GameObject target, StatusEffect effect)
    {
        StopCoroutine(StatusEffectHandler(target, effect));
        
    }
   

    public IEnumerator StatusEffectHandler(GameObject target,StatusEffect effect)
    {
        effect.OnApply(target);
        yield return new WaitForSeconds(effect.duration);
        StopEffectCoroutine(target, effect);
        RemoveStatusEffect(target, effect);
        effect.OnRemove(target);

    }
}
