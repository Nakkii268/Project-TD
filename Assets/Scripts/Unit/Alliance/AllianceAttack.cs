using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceAttack : MonoBehaviour
{
    [SerializeField] private Alliance alliance;
    [SerializeField] private float allyAttack;
    [SerializeField] private List<GameObject> targets;
    public event EventHandler OnAttackPerform;

    private void Start()
    {
        alliance.AllienceAttackCollider.OnEnemyIn += AllienceAttackCollider_OnEnemyIn;
        alliance.AllienceAttackCollider.OnEnemyOut += AllienceAttackCollider_OnEnemyOut;
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

    public void AttackPerform()
    {
        Debug.Log("attack");
        OnAttackPerform?.Invoke(this,EventArgs.Empty);
    }

    public void AddTarget(GameObject enemy)
    {
        targets.Add(enemy); 
    }
    public void RemoveTarget(GameObject enemy) 
    {
        targets.Remove(enemy);
    }

    
}
