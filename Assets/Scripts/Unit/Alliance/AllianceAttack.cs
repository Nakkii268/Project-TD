using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AllianceAttack : MonoBehaviour, IAttackPerform
{
    [SerializeField] protected Alliance alliance;
    public Alliance Alliance {  get { return alliance; } }
   
    [SerializeField] protected List<GameObject> targets;
    [SerializeField] protected List<GameObject> currentTarget;
    public List<GameObject> CurrentTarget { get { return currentTarget; } }

    [SerializeField] protected TargetCount targetCount;
    [SerializeField] protected DamageType damageType;
    [SerializeField] protected bool attackReady {  get; private set; }
    public List<GameObject> targetsInRange { get { return targets; } }

    public event EventHandler<List<GameObject>> OnAttackPerform;
    public event EventHandler OnNoEnemy;
   

    protected virtual void Start()
    {
        alliance.AllienceAttackCollider.OnTargetIn += AllienceAttackCollider_OnEnemyIn;
        alliance.AllienceAttackCollider.OnTargetOut += AllienceAttackCollider_OnEnemyOut;
        targetCount = alliance.GetAllianceUnit().TargetCount;
        damageType = alliance.GetAllianceUnit().DamageType;
        
        attackReady = true;
        
    }

    private void AllienceAttackCollider_OnEnemyOut(object sender, GameObject e)
    {
        if (currentTarget.Contains(e))
        {
            RemoveTarget(e);
            GetTarget();
        }
        else
        {
            RemoveTarget(e);
        }
        if (!IsHaveTarget()) //interupt attack if target out when attack already perform but still not dmg target yet (or may be already dmg)
        {
            OnNoEnemy?.Invoke(this, EventArgs.Empty); 
        }
    }

    private void AllienceAttackCollider_OnEnemyIn(object sender, GameObject e)
    {
        AddTarget(e);
        GetTarget();
       
    }

    public virtual void AttackPerform()
    {
        
        OnAttackPerform?.Invoke(this,currentTarget);
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
        if(targets.Count == 0)
        {
            currentTarget = null;
        }
    }

    protected List<GameObject> GetClosestTarget() {
        if (targets.Count == 0) return null; 
        List<GameObject> toReturn = new();
        GameObject min = targets[0];
        for (int i = 1; i < targets.Count; i++) {
            if (Vector2.Distance(targets[i].transform.position,transform.position) < Vector2.Distance(min.transform.position, transform.position))
            {
                min = targets[i];
            }
        }
        
        toReturn.Add(min);  
        return toReturn;
    }
    protected GameObject GetMinHpTarget()//remind to change
    {
        if (targets.Count == 0) return null;
       GameObject toReturn = new();

        GameObject min = targets[0];
        for (int i = 1; i < targets.Count; i++)
        {
            if (targets[i].GetComponent<IHealable>().GetPercentHp() < min.GetComponent<IHealable>().GetPercentHp())
            {
                min = targets[i];
            }
            toReturn = min;
        }
        
        return toReturn;
    }
    protected void GetTarget()
    {
        
        if(targetCount == TargetCount.Single)
        {
            if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Enemy)
            {
                currentTarget= GetClosestTarget();


            }
            else if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Alliance)
            {
                //currentTarget= GetMinHpTarget(); 

            }
        }
        else if(targetCount == TargetCount.Blocked)
        {
            currentTarget = alliance.AllianceBlock.GetBlockedEnemy();

        }else if(targetCount == TargetCount.AllInRange)
        {
            currentTarget= targets;
        }

        
    }

    

    public void SetTargetCount(TargetCount tc)
    {
        targetCount = tc;
    }
    private IEnumerator AttackCoolDown(float AttackSpeed)
    {
        
        yield return new WaitForSeconds(AttackSpeed);
        attackReady = true;
        
    }

    
    
}
public enum TargetCount
{
    Single,
    Blocked,
    AllInRange
}