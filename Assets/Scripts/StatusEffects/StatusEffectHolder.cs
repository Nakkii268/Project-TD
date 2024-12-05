using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHolder : MonoBehaviour
{
    public StatusEffect[] effects;


    public void StartEffectCoroutine(IEnumerator effect)
    {
        StartCoroutine(effect);
    }
    public void StopEffectCoroutine(IEnumerator effect)
    {
        StopCoroutine(effect);
    }
}
