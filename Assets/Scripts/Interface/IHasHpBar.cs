using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasHpBar 
{
    public event EventHandler<float> OnHpChange;
}
