using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alliance : Character, IDamageable, IHealable, IHasHpBar
{
    [SerializeField] private Vector2 unitPos;
    public Vector2 UnitPos { get { return unitPos; } }
    [SerializeField] private AllianceUnit unit;
    [SerializeField] public Vector2 direction {  get; private set; }
    [SerializeField] private bool isDeloyed;


    [SerializeField] private AllianceVisual allianceVisual;
    public AllianceVisual AllianceVisual { get { return allianceVisual; } }

    [SerializeField] private AllianceDirection allianceDirection;
    public AllianceDirection AllianceDirection { get { return allianceDirection; } }

    [SerializeField] private AllianceAttackRange allianceAttackRange;
    public AllianceAttackRange AllianceAttackRange { get { return allianceAttackRange; } }
    [SerializeField] private AllianceInfomation allianceInfo;

    [SerializeField] private AllianceAttackCollider allienceAttackCollider;
    public AllianceAttackCollider AllienceAttackCollider { get { return allienceAttackCollider; } }

    [SerializeField] private AllianceStat allianceStat;
    public AllianceStat Stat { get { return allianceStat; } }

    [SerializeField] private AllianceSkill allianceSkill;
    public AllianceSkill AllianceSkill { get { return allianceSkill; } }

    [SerializeField] private AllianceAttack allianceAttack;
    public AllianceAttack AllianceAttack { get { return allianceAttack; } }

    [SerializeField] private AllianceBlock allianceBlock;
    public AllianceBlock AllianceBlock { get {  return allianceBlock; } }
    
    

    [SerializeField] private AllianceDirectionCircle directionCircle;
    [SerializeField] private AllianceHeathBar heathBar;
    [SerializeField] private StatusEffectHolder statusEffectHolder;
    public StatusEffectHolder StatusEffectHolder { get {    return statusEffectHolder; } }  

    //state machine
    [SerializeField] private AllianceSMManager stateMachine; 
    public AllianceSMManager StateMachine { get { return stateMachine; } }
    

    public Collider UnitUICollider;
    public int charIndex;

    public event EventHandler OnGetHit;
    public event EventHandler OnUnitRetreat;
    public event EventHandler<float> OnHpChange;
    public event EventHandler OnUnitDead;
   
    private void Awake()
    {
        stateMachine = new AllianceSMManager(this);

    }
    private void Start()
    {
    
        allianceDirection.OnDeloyed += AllianceDirection_OnDeloyed;
       
        LevelManager.instance.MapManager.OnClickOtherTarget += LevelManager_OnClickOtherTarget;
        allianceBlock.BlockCollider.enabled = false;

    }
    private void Update()
    {
        stateMachine.Update();
    }
   
    private void LevelManager_OnClickOtherTarget(object sender, bool e)
    {
        if (allianceInfo == null) return;
        if (!isDeloyed)
        {
            Retreat(false);
        }
        if (!allianceInfo.gameObject.activeInHierarchy) return;
        
        if (allianceInfo.IsPointerIn()) return;

        if (!e) // not other unit then set rotation back
        {
            CameraManager.instance.SetCameraOriginRotation();

        }
        UIHide();
    }

    
    private void AllianceDirection_OnDeloyed(object sender, Vector2 e) // just drop to block not really deploy
    {
        isDeloyed = true;
        direction = e;
        UnitUICollider.gameObject.SetActive(false);
        directionCircle.gameObject.SetActive(true);
        directionCircle.Initialize(e);
        InGameCharListUI.Instance.HideDeloyedUnitUI(charIndex);

    }

    public AllianceUnit GetAllianceUnit()
    {
        return unit;
    }



    public Vector2 GetUnitDir()
    {
        return direction;
    }

    public Vector2 GetUnitPos()
    {
        return unitPos;
    }
    public int GetUnitCost()
    {
        return unit.UnitDp;
    }
    public int GetUnitIndex()
    {
        return charIndex;
    }
    
    public void SetUnit(Unit u,Vector2 pos, int indx)
    {
        unitPos = pos;
        unit = (AllianceUnit)u;
        charIndex = indx;
    }
    public void UnitDeloy(Vector2 dir) {
        allianceDirection.gameObject.SetActive(false);
        direction = dir;
        allianceAttackRange.SetAttackRange(dir);
        allienceAttackCollider.SetCollider(allianceAttackRange.AttackRange,unitPos);
        stateMachine.ChangeState(stateMachine.AllianceIdleState);
        allianceVisual.SetSortingOrder(unitPos.y);
        allianceBlock.BlockCollider.enabled = true;

    }
    public void Retreat(bool isRetreat)
    {
        if (isRetreat)
        {
            InGameCharListUI.Instance.ShowRetreatedUnitUI(charIndex);
        }
        isDeloyed = false;
        //directionCircle.gameObject.SetActive(false);
        LevelManager.instance.MapManager.UnHighLightBlockList(allianceAttackRange.AttackRange);
        Block block = GetComponentInParent<Block>();
        block.UnitReTreat();
        
        CameraManager.instance.SetCameraOriginRotation();
        OnUnitRetreat?.Invoke(this, EventArgs.Empty);

    }

    //ui infomation
    public void UIShowOnForcus()
    {
        if (isDeloyed)
        {

            allianceInfo.gameObject.SetActive(true);
            LevelManager.instance.MapManager.HighLightBlockList(allianceAttackRange.AttackRange, 8);
            UnitUICollider.gameObject.SetActive(true);
            LevelManager.instance.TimeSlow();
        }
    }
    public void UIHide()
    {
        allianceInfo.gameObject.SetActive(false);
        allianceDirection.gameObject.SetActive(false);
        LevelManager.instance.MapManager.UnHighLightBlockList(allianceAttackRange.AttackRange);
        UnitUICollider.gameObject.SetActive(false);
        LevelManager.instance.TimeNormal();
    }
    //
    public void ReceiveDamaged(float damage,DamageType type)
    {
        if(type == DamageType.MagicDamage)
        {
            
            allianceStat.currentHp -= (damage - damage * (GetReductionValue(allianceStat.Resistance.Value)));

            OnGetHit?.Invoke(this, EventArgs.Empty);

        }else if(type == DamageType.PhysicDamage)
        {
            allianceStat.currentHp -= (damage - damage * (GetReductionValue(allianceStat.Defense.Value)));

            OnGetHit?.Invoke(this, EventArgs.Empty);
        }
        else if(type == DamageType.TrueDamage)
        {
            allianceStat.currentHp -= damage;

            OnGetHit?.Invoke(this, EventArgs.Empty);
            
        }
        
        OnHpChange?.Invoke(this, allianceStat.currentHp / allianceStat.MaxHp.Value);

        if (allianceStat.currentHp <= 0)
        {
            allianceStat.currentHp = 0;
            OnUnitDead?.Invoke(this, EventArgs.Empty);
            
        }

    }

    public void Heal(float amout)
    {
        allianceStat.currentHp += amout;

        OnHpChange?.Invoke(this, allianceStat.currentHp / allianceStat.MaxHp.Value);

        if (allianceStat.currentHp >= allianceStat.MaxHp.Value)
        {
            allianceStat.currentHp = allianceStat.MaxHp.Value;

        }
    }

    public float GetReductionValue(float def)
    {
        if (def / 100 >= 1) return (99 / 100); // make sure dmg reduce wont exceed 99%
        return def / 100;

    }
    public void Use() //test func
    {
        allianceVisual.PlayDeadAnim();
    }

    public float GetPercentHp()
    {
        return Stat.currentHp / Stat.MaxHp.Value;
    }
    
    public Quaternion GetVFXQuaternion()
    {
        if (direction == new Vector2(-1, 0))
        {
            return Quaternion.Euler(-90, 90, -90);

        }
        else if (direction == new Vector2(1, 0))
        {
            return Quaternion.Euler(90, 90, -90);

        }
        else if (direction == new Vector2(0, -1))
        {
            return Quaternion.Euler(180, 90, -90);

        }
        else if (direction == new Vector2(0, 1))
        {
            return Quaternion.Euler(0, 90, -90);

        }
        return Quaternion.Euler(0, 0, 0);

    }

}
