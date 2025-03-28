using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Enemy : Character, IDamageable, IHealable, IHasHpBar
{
  


    [SerializeField] private Vector2[] path;
    public Vector2[] Path { get { return path; } }
    [SerializeField] public int pathIndex = 0;
    [SerializeField] private bool isBlocked;
    [SerializeField] public bool isWaveEnemy;
    public bool IsBlocked {  get { return isBlocked; } }

    [SerializeField] private bool isMoving;
    public bool IsMoving {  get { return isMoving; }set { isMoving = value; } }
    [SerializeField] private EnemyVisual _enemyVisual;
    public EnemyVisual EnemyVisual { get { return _enemyVisual; } }
    [SerializeField] private EnemyUnit unit;
     public EnemyUnit Unit { get { return unit; } }
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

    public event EventHandler<EnemyDeadArg> OnEnemyDead;
    public event EventHandler OnGetHit;
    public event EventHandler<float> OnHpChange;
    public GameObject Blocker;

    private void Awake()
    {
        eStateMachine = new EnemySMManager(this);

    }

    private void Start()
    {

        eStateMachine.ChangeState(eStateMachine.EnemyIdleState);
       

    }

    private void Update()
    {
        eStateMachine.Update();
      

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
            stat.currentHp -= (damage - damage * (GetReductionValue(stat.Resistance.Value)));
        }
        else if (type == DamageType.PhysicDamage)
        {
            stat.currentHp -= (damage - damage * (GetReductionValue(stat.Defense.Value)));


        }
        else if (type == DamageType.TrueDamage)
        {
            stat.currentHp -= damage;

        }
        if (stat.currentHp < 0)
        {
            stat.currentHp = 0;
            OnEnemyDead?.Invoke(this, new EnemyDeadArg(this.gameObject, isWaveEnemy));
            Debug.Log("dead");
        }
        LevelManager.instance.ParticleManager.HitParticle(this.gameObject);

        OnHpChange?.Invoke(this, stat.currentHp / stat.MaxHp.Value);
    }

  
    public void Blocked(GameObject blocker)
    {
        Blocker = blocker;
        //stat.Speed.Value = 0;
    }
    public void UnBlock(GameObject blocker)
    {
        Blocker=null;
        
    }
    public void DeadBtn()
    {
        OnEnemyDead?.Invoke(this, new EnemyDeadArg(this.gameObject,isWaveEnemy));
    }

    public float GetPercentHp()
    {
        return stat.currentHp / stat.MaxHp.Value;
    }
    public Vector2 GetNextTarget()
    {
        return path[pathIndex+1];
    }
    public void SetPath(Vector2[] p)
    {
        path = p;
    }
    public void SetUnit(EnemyUnit u)
    {
        unit = u;
    }
    public void Dead()
    {
        Debug.Log("destroyed");
        Destroy(gameObject);
    }
    public float GetReductionValue(float def)
    {
        if (def / 100 >= 1) return (99 / 100); // make sure dmg reduce wont exceed 99%
        return def / 100;

    }
}
public class EnemyDeadArg
{
   public GameObject enemy;
   public  bool isWave;
    public EnemyDeadArg(GameObject enemy, bool isWave)
    {
        this.enemy = enemy;
        this.isWave = isWave;
    }
}