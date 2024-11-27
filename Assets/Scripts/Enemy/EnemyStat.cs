using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IDamageable
{
    public EnemyUnit eUnit;
    public int unitLevel;
    public float CurrentHp;
    public float MaxHp;

    private void Start()
    {
      //  MaxHp = eUnit.Heath.GetFinalStat(unitLevel); //only base stat, no modifier
       // CurrentHp = eUnit.Heath.GetFinalStat(unitLevel); //only base stat, no modifier
    }

    public void ReceiveDamaged(float damage)
    {
        if (CurrentHp <= 0) return;
        CurrentHp -= damage;    
    }

}
