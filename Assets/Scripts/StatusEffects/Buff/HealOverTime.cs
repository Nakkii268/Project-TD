using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "StatusEffect/HealOverTime")]

public class HealOverTime : NormalStatusEffect
{
    public float HealAmount;

    
    public override void OnApply(GameObject target)
    {
        Alliance tg = target.GetComponentInParent<Alliance>();

        tg.StartCoroutine(Heal(HealAmount, duration, tg));
    }
    public override void OnRemove(GameObject target)
    {
       Alliance tg= target.GetComponentInParent<Alliance>();
            tg.StopCoroutine(Heal(HealAmount,duration,tg));
    }
    public IEnumerator Heal(float amt, float drt,Alliance tg)
    {
        float elapseTime = 0f;
        while (elapseTime < drt)
        {
            float heal=(amt/drt)*Time.deltaTime;
             tg.Heal(heal);
            elapseTime += Time.deltaTime;
            yield return null;
        }

    }
}