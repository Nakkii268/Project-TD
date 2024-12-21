using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllianceAttack : MonoBehaviour, IAttackPerform
{
    [SerializeField] private Alliance alliance;
    [SerializeField] private float allyAttack;
    [SerializeField] private List<GameObject> targets;
    public bool attackReady {  get; private set; }
    public List<GameObject> targetsInRange { get { return targets; } }

    public event EventHandler<List<GameObject>> OnAttackPerform;
   

    private void Start()
    {
        alliance.AllienceAttackCollider.OnTargetIn += AllienceAttackCollider_OnEnemyIn;
        alliance.AllienceAttackCollider.OnTargetOut += AllienceAttackCollider_OnEnemyOut;
    }

    private void AllienceAttackCollider_OnEnemyOut(object sender, GameObject e)
    {
        RemoveTarget(e);
    }

    private void AllienceAttackCollider_OnEnemyIn(object sender, GameObject e)
    {
        AddTarget(e);
        AttackPerform();
    }

    public void AttackPerform()//get attack.value right from stat
    {
        Debug.Log("attack");
        if (alliance.GetAllianceUnit().UnitTarget == UnitTarget.Enemy) 
        {
            //attack enemy
    
        }
        else if(alliance.GetAllianceUnit().UnitTarget == UnitTarget.Alliance)
        {
            //heal alliance

        }
        else if(alliance.GetAllianceUnit().UnitTarget == UnitTarget.Both)
        {
            //do both

        }
        OnAttackPerform?.Invoke(this,targets);
        attackReady = false;
        StartCoroutine(AttackCoolDown(alliance.Stat.AttackInterval.Value));
    }
    public bool IsHaveTarget()
    {
        return targetsInRange.Count > 0;
    }
    public void AddTarget(GameObject enemy)
    {
        targets.Add(enemy); 
    }
    public void RemoveTarget(GameObject enemy) 
    {
        targets.Remove(enemy);
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
    }
    
}
