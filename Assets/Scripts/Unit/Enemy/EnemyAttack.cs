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
    protected GameObject GetClosestTarget()
    {
        
        GameObject min = targets[0];
        for (int i = 1; i < targets.Count; i++)
        {
            if (Vector2.Distance(targets[i].transform.position, transform.position) < Vector2.Distance(min.transform.position, transform.position))
            {
                min = targets[i];
            }
        }

       
        return min;
    }

}
