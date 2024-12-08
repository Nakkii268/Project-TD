using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public event EventHandler OnGetHit;

    public void ReceiveDamaged(float damage,DamageType type);
}
