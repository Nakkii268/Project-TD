using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Enemy : Character, IDamageable, IHealable, IHasHpBar
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Vector3 target;
    [SerializeField] private Vector2[] path;
    [SerializeField] private int pathIndex = 0;
    [SerializeField] private bool isBlocked;
    public bool IsBlocked {  get { return isBlocked; } }

    [SerializeField] private EnemyStat stat;
    public EnemyStat Stat { get { return stat; } }

    [SerializeField] private EnemyAttack enemyAttack;
    public EnemyAttack EnemyAttack { get { return enemyAttack; } }

    [SerializeField] private EnemyAttackCollider enemyAttackCollider;
    public EnemyAttackCollider EnemyAttackCollider {  get { return enemyAttackCollider; } }
    [SerializeField] private StatusEffectHolder statusEffectHolder;
    public StatusEffectHolder StatusEffectHolder { get {    return statusEffectHolder; } }

    [SerializeField] private EnemySMManager eStateMachine;
    public EnemySMManager EnemySMManager { get { return eStateMachine; } }

    public event EventHandler<GameObject> OnEnemyDead;
    public event EventHandler OnGetHit;
    public event EventHandler<float> OnHpChange;

    private void Awake()
    {
        eStateMachine = new EnemySMManager(this);

    }

    private void Start()
    {

        target = path[pathIndex];
        
    }

    private void Update()
    {
       //eStateMachine.Update();
        if (path.Length == 0) return;
        target = new Vector3(path[pathIndex].x, path[pathIndex].y, transform.position.z);
       transform.position= Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //move
        if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(target.x, target.y, 0)) < .3f)
        {
            if (pathIndex >= path.Length - 1) return;
            pathIndex++;
            target = path[pathIndex];

        }
        Vector2 dir = (Vector2)target - new Vector2(transform.position.x, transform.position.y);
        
        


    }
    private void FixedUpdate()
    {
        eStateMachine?.FixedUpate();
    }

    public void Heal(float amout)
    {
        stat.currentHp += amout;
        if (stat.currentHp >= stat.MaxHp.Value)
        {
            stat.currentHp = stat.MaxHp.Value;
        }
    }

    public void ReceiveDamaged(float damage, DamageType type)
    {
        if (type == DamageType.MagicDamage)
        {
            stat.currentHp -= (damage - damage * (stat.Resistance.Value / 100));
        }
        else if (type == DamageType.PhysicDamage)
        {
            stat.currentHp -= (damage - damage * (stat.Defense.Value / 100));


        }
        else if (type == DamageType.TrueDamage)
        {
            stat.currentHp -= damage;

        }
        if (stat.currentHp < 0)
        {
            stat.currentHp = 0;
        }
        OnHpChange?.Invoke(this, stat.currentHp / stat.MaxHp.Value);
    }

  
    public void Blocked()
    {
        
        speed *= 0;
    }
    public void UnBlock()
    {
        
        speed *= 1f;
    }
    public void DeadBtn()
    {
        OnEnemyDead?.Invoke(this, this.gameObject);
    }

    public float GetPercentHp()
    {
        return stat.currentHp / stat.MaxHp.Value;
    }
    public Vector2 GetNextTarget()
    {
        return path[pathIndex+1];
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
   
}
