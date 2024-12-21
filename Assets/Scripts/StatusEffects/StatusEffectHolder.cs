using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StatusEffectHolder : MonoBehaviour
{
    public Character holder;   
    public List<NormalStatusEffect> NorEffect;
    public List<OnHitStatusEffect> OnhitEffects;
    public event EventHandler OnGetDisable;
    public event EventHandler OnEndDisable;

    private void Start()
    {
        holder.GetComponentInChildren<IAttackPerform>().OnAttackPerform += AttackPerformer_OnAttackPerform;
        
    }

    private void AttackPerformer_OnAttackPerform(object sender, List<GameObject> e)
    {
        if (OnhitEffects == null) return;
        for (int i = 0; i < OnhitEffects.Count; i++)
        {
            for (int j = 0; i < e.Count; j++)
            {
                OnhitEffects[i].OnApply(this.gameObject, e[j]);
            }
        }
    }


    //add-remove
    public void AddStatusEffect(GameObject target,StatusEffect effect)
    {
        if (effect.SType == StatusType.Normal)
        {
            NormalStatusEffect Neffect = (NormalStatusEffect)effect;   
            if (Neffect.Stackable)
            {
                NorEffect.Add(Neffect);
                if (Neffect.duration > 99)// for effect have unlimited duration, set duration >99=> only apply and wont remove auto
                {
                    Neffect.OnApply(target);
                }
                StartEffectCoroutine(target, Neffect);
                return;
            }
            //make sure status effect not stack

            if (!NorEffect.Contains(Neffect))
            {
                NorEffect.Add(Neffect);
                if (effect.duration > 99)// for effect have unlimited duration, set duration >99=> only apply and wont remove auto
                {
                    Neffect.OnApply(target);
                    return;
                }
                StartEffectCoroutine(target, Neffect);
            }
            else
            {
                StopEffectCoroutine(target, Neffect);
                Neffect.OnRemove(target);
                StartEffectCoroutine(target, Neffect);

            }
        }else if(effect.SType == StatusType.OnHit)
        {
            OnHitStatusEffect OHeffect = (OnHitStatusEffect)effect;

            if (OHeffect.Stackable)
            {
                OnhitEffects.Add(OHeffect);
                if (OHeffect.duration > 99) return;
                StartEffectCoroutine(target, OHeffect);
                return;
            }
            if (!OnhitEffects.Contains(OHeffect))
            {
                OnhitEffects.Add(OHeffect);
                if (effect.duration > 99)// for effect have unlimited duration, set duration >99=> only apply and wont remove auto
                {
                    OHeffect.OnApply(target);
                    return;
                }
                StartEffectCoroutine(target, OHeffect);
            }
            else
            {
                return;
            }
        }
    }

    public void RemoveStatusEffect(StatusEffect effect)
    {
        if (effect.SType == StatusType.Normal)
        {
            NormalStatusEffect Neffect = (NormalStatusEffect)effect;

            if (!NorEffect.Contains(Neffect))
            {
                return;
            }
            NorEffect.Remove(Neffect);
        }else if( effect.SType == StatusType.OnHit)
        {
            OnHitStatusEffect OHeffect = (OnHitStatusEffect)effect;

            if (!OnhitEffects.Contains(OHeffect))
            {
                return;
            }
            OnhitEffects.Remove(OHeffect);
        }

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
        RemoveStatusEffect( effect);
        effect.OnRemove(target);

    }

    

}
