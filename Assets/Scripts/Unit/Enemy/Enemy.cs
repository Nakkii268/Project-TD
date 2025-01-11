using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Enemy : Character, IDamageable, IHealable
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Vector3 target;
    [SerializeField] private Vector3[] path;
    [SerializeField] private int pathIndex = 0;
    [SerializeField] private EnemyStat stat;
    public EnemyStat Stat { get { return stat; } }

    [SerializeField] private EnemyAttack enemyAttack;
    public EnemyAttack EnemyAttack { get { return enemyAttack; } }
    [SerializeField] private EnemySMManager eStateMachine;
    public EnemySMManager EnemySMManager { get { return eStateMachine; } }

    public event EventHandler<GameObject> OnEnemyDead;
    public event EventHandler OnGetHit;



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
       eStateMachine.Update();
        if(Vector3.Distance(new Vector3( transform.position.x, transform.position.y,0), new Vector3(target.x,target.y,0)) < .1f)
        {
            if (pathIndex >= path.Length-1) return;
            pathIndex++;
            target = path[pathIndex];

        }
        Vector2 dir = (Vector2)target - new Vector2(transform.position.x, transform.position.y);
        transform.Translate(speed * Time.deltaTime * (Vector3)dir);
        
        
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

    public void Heal(float amout, float Scale)
    {
        throw new NotImplementedException();
    }
}
