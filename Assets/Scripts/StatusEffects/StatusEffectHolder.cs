using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StatusEffectHolder : MonoBehaviour
{
    public List<StatusEffect> effects;

    //add-remove
    public void AddStatusEffect(StatusEffect effect)
    {
        //make sure status effect not stack
        if (!effects.Contains(effect)) 
        {
            effects.Add(effect);
            StartEffectCoroutine(effect);
        }
        else
        {
            StopEffectCoroutine(effect);
            StartEffectCoroutine(effect);

        }
    }

    public void RemoveStatusEffect(StatusEffect effect)
    {
        if (!effects.Contains(effect)) return;
        effects.Remove(effect);
    }

    //start- stop
    public void StartEffectCoroutine(StatusEffect effect)
    {
        StartCoroutine(StatusEffectHandler(effect));
    }
    public void StopEffectCoroutine(StatusEffect effect)
    {
        StopCoroutine(StatusEffectHandler(effect));
        
    }
   

    public IEnumerator StatusEffectHandler(StatusEffect effect)
    {
        effect.OnApply();
        yield return new WaitForSeconds(effect.duration);
        StopEffectCoroutine(effect);
        RemoveStatusEffect(effect);
    }
}
