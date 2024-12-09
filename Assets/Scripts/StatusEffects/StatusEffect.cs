using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatusEffect : ScriptableObject
{
    public float duration;
    public bool Stackable;
    public virtual void OnApply(GameObject target) { }
    public virtual void OnRemove(GameObject target) { }
   
}
