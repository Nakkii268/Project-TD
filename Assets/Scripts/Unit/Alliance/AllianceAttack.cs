using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllianceAttack : MonoBehaviour, IAttackPerform
{
    [SerializeField] protected Alliance alliance;
    public Alliance Alliance {  get { return alliance; } }
    [SerializeField] protected float allyAttack;
    [SerializeField] protected List<GameObject> targets;
    [SerializeField] protected TargetCount targetCount;
    [SerializeField] protected DamageType damageType;
    [SerializeField] protected bool attackReady {  get; private set; }
    public List<GameObject> targetsInRange { get { return targets; } }

    public event EventHandler<List<GameObject>> OnAttackPerform;
   

    private void Start()
    {
        alliance.AllienceAttackCollider.OnTargetIn += AllienceAttackCollider_OnEnemyIn;
        alliance.AllienceAttackCollider.OnTargetOut += AllienceAttackCollider_OnEnemyOut;
        targetCount = alliance.GetAllianceUnit().TargetCount;
        damageType = alliance.GetAllianceUnit().DamageType;
        attackReady = true;
    }

    private void AllienceAttackCollider_OnEnemyOut(object sender, GameObject e)
    {
        RemoveTarget(e);
    }

    private void AllienceAttackCollider_OnEnemyIn(object sender, GameObject e)
    {
        AddTarget(e);
       
    }

    public virtual void AttackPerform()
    {
        
        OnAttackPerform?.Invoke(this,targets);
        attackReady = false;
        StartCoroutine(AttackCoolDown(alliance.Stat.AttackInterval.Value));
    }

    public virtual void Attack() { }
    public bool IsHaveTarget()
    {
        return targetsInRange.Count > 0;
    }
    public bool CanPerformAttack()
    {
        return IsHaveTarget() && attackReady;
    }
    public void AddTarget(GameObject enemy)
    {
        targets.Add(enemy); 
    }
    public void RemoveTarget(GameObject enemy) 
    {
        targets.Remove(enemy);
    }

    protected List<GameObject> GetMinHpTarget() {
        List<GameObject> toReturn = new List<GameObject>();
        GameObject min = targets[0];
        for (int i = 1; i < targets.Count; i++) {
            if (targets[i].GetComponent<IDamageable>().GetPercentHp() < min.GetComponent<IDamageable>().GetPercentHp())
            {
                min = targets[i];
            }
        }
        toReturn.Add(min);  
        return toReturn;
    }
    protected List<GameObject> GetTarget()
    {
        if(targetCount == TargetCount.Single)
        {
            return GetMinHpTarget();
        }
        else if(targetCount == TargetCount.Blocked)
        {
            return alliance.AllianceBlock.GetBlockedEnemy();
        }else if(targetCount == TargetCount.Blocked)
        {
            return targets;
        }
        return null;
    }

    public void SetTargetCount(TargetCount tc)
    {
        targetCount = tc;
    }
    private IEnumerator AttackCoolDown(float AttackSpeed)
    {
        float attackInterval=0;
          
        while (attackInterval < AttackSpeed)
        {
            attackInterval += Time.deltaTime;
            yield return null;
        }
        attackReady = true;
        Debug.Log("ready");
    }

    
    
}
public enum TargetCount
{
    Single,
    Blocked,
    AllInRange
}