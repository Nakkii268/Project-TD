using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IAttackPerform 
{
    public event EventHandler<List<GameObject>> OnAttackPerform;
}
