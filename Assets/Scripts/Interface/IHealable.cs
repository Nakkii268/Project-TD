using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IHealable 
{
    public void Heal(float amout);
    public float GetPercentHp();
    

}
