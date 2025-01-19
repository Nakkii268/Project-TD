using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IAttackPerform
{
    protected Enemy m_Enemy;
    protected bool attackReady;
    public bool AttackReady { get { return attackReady; } } 
    protected float attackRange;
    public float AttackRange { get { return attackRange; } }
    protected float attackDmg;
    protected DamageType damageType;
    public event EventHandler<List<GameObject>> OnAttackPerform;
    public  List<GameObject> targets;

    
    protected virtual void PerformAttack()
    {
        OnAttackPerform?.Invoke(this,targets );
        AttackCoolDown(m_Enemy.Stat.AttackInterval.Value);
    }
    public virtual void Attack(GameObject target) { }


    private IEnumerator AttackCoolDown(float AttackSpeed)
    {

        yield return new WaitForSeconds(AttackSpeed);
        attackReady = true;
       
    }
    
}
