using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatusEffect : ScriptableObject
{
    public float duration;
    public bool Stackable;
    public StatusType SType;
    public virtual void OnApply(GameObject target) { }
    public virtual void OnApply(GameObject holder, GameObject target) { }
    public virtual void OnRemove(GameObject target) { }
    
    
   
}
public enum StatusType
{
    Normal,
    OnHit
}