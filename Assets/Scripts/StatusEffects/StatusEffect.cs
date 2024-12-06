using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatusEffect : ScriptableObject
{
    public float duration;
    public virtual void OnApply() { }
   
}
