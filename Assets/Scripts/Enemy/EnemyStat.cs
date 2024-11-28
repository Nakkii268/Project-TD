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
        
    }

    public void ReceiveDamaged(float damage)
    {
        if (CurrentHp <= 0) return;
        CurrentHp -= damage;    
    }

}
