using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealable 
{
    public void Heal(float amout);
    public float GetPercentHp();


}
