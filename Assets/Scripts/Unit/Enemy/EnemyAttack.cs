using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IAttackPerform
{
    [SerializeField] protected Enemy m_Enemy;
    [SerializeField] protected bool attackReady;
    public bool AttackReady { get { return attackReady; } } 
    [SerializeField] protected float attackRange;
    public float AttackRange { get { return attackRange; } }

    [SerializeField] protected DamageType damageType;
    public event EventHandler<List<GameObject>> OnAttackPerform;
    public  List<GameObject> targetsInRange;
    public GameObject target;


    private void Start()
    {
        
        m_Enemy.EnemyAttackCollider.OnTargetIn += EnemyAttackCollider_OnTargetIn;
        m_Enemy.EnemyAttackCollider.OnTargetOut += EnemyAttackCollider_OnTargetOut;
    }

    private void EnemyAttackCollider_OnTargetOut(object sender, GameObject e)
    {
        if (target == e)
        {
            target = null;
        }
        targetsInRange.Remove(e);
        target = GetTarget();
    }

    private void EnemyAttackCollider_OnTargetIn(object sender, GameObject e)
    {
        targetsInRange.Add(e);
        target = GetTarget();

    }

    private void EnemyAttackCollider_OnDetectEnemy(object sender, Collider2D[] e)
    {
        for (int i = 0; i < e.Length; i++) {
            targetsInRange.Add(e[i].gameObject);
        }
    }

    public virtual void PerformAttack()
    {
        OnAttackPerform?.Invoke(this,targetsInRange );
        attackReady = false;
        StartCoroutine(AttackCoolDown(m_Enemy.Stat.AttackInterval.Value));
    }
    public virtual void Attack() { }


    private IEnumerator AttackCoolDown(float AttackSpeed)
    {

        yield return new WaitForSeconds(AttackSpeed);
        attackReady = true;
       
    }
    public bool CanPerformAttack()
    {
        return attackReady && targetsInRange.Count >0;
    }
    protected GameObject GetClosestTarget()
    {
        if (targetsInRange.Count == 0) return null;
        GameObject min = targetsInRange[0];
        for (int i = 1; i < targetsInRange.Count; i++)
        {
            if (Vector2.Distance(targetsInRange[i].transform.position, transform.position) < Vector2.Distance(min.transform.position, transform.position))
            {
                min = targetsInRange[i];
            }
        }

       
        return min;
    }
    private GameObject GetTarget()
    {
        if (targetsInRange.Count == 0) return null;
        if (m_Enemy.IsBlocked)
        {
            return m_Enemy.Blocker;
        }
        else
        {
            return GetClosestTarget();
        }
    }
  
}
