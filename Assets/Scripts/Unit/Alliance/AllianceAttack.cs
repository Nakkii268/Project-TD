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

    [SerializeField] protected Transform vfxPos;


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
        if (targets.Contains(e))
        {
            RemoveTarget(e);
            SortingTarget(targets,0,targets.Count-1);
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
        SortingTarget(targets, 0, targets.Count-1);


    }

    public virtual void AttackPerform()
    {
        
        OnAttackPerform?.Invoke(this,GetTarget());
        attackReady = false;
        StartCoroutine(AttackCoolDown(alliance.Stat.AttackInterval.Value));
    }

    public virtual void Attack() { }
    public bool IsHaveTarget()
    {
        if (alliance.GetAllianceUnit().UnitTarget == UnitTarget.Alliance )
        {
            for (int i = 0; i < targetsInRange.Count; i++)
            {
                if (targetsInRange[i].GetComponentInParent<IHealable>().GetPercentHp() <= .99f)
                {
                    
                    return true;
                }
            }
            return false;

        }

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

   
    
    public List<GameObject> GetTarget()
    {
        
        if(targetCount == TargetCount.Single)
        {   
            
            return new List<GameObject>() { targets[0] };

        }
        else if(targetCount == TargetCount.Blocked)
        {

            return alliance.AllianceBlock.GetBlockedEnemy();

        }
        else if(targetCount == TargetCount.AllInRange)
        {
           
            return targets;

        }

        return null;

        
    }

    public List<GameObject> GetMinHpTarget()
    {
        List<IHealable> targetHps = new List<IHealable>();
        for (int i = 0; i < targets.Count; i++)
        {
            targetHps.Add(targets[i].GetComponentInParent<IHealable>());
        }
       
        int minIndex = 0;
        for (int i = 0; i < targetHps.Count; i++)
        {
            if (targetHps[i].GetPercentHp() < targetHps[minIndex].GetPercentHp())
            {
                
                minIndex = i;
            }
        }
        return new List<GameObject> { targets[minIndex] };
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

    private void SortingTarget(List<GameObject> tgs, int low, int hight)
    {
        List<IHealable> targetHps = new List<IHealable>();
        for (int i = 0; i < tgs.Count; i++)
        {
            targetHps.Add(tgs[i].GetComponentInParent<IHealable>());
        }
        if(low < hight)
        {
            int pivotIndex = Partition(targetHps, low,hight);
            SortingTarget(tgs,low,pivotIndex-1);
            SortingTarget(tgs, pivotIndex+1, hight);
        }

    }
    private int Partition(List<IHealable> tgs, int low, int hight)
    {
        
        float pivot = tgs[hight].GetPercentHp();

        int i = low - 1;
        for (int j = low; j < hight; j++)
        {
            if (tgs[j].GetPercentHp() < pivot)
            {
                i++;
                (tgs[j], tgs[i]) = (tgs[i], tgs[j]);
            }
        }
        (tgs[i + 1], tgs[hight]) = (tgs[hight], tgs[i + 1]);
        return i + 1;

    }
}
public enum TargetCount
{
    Single,
    Blocked,
    AllInRange
}