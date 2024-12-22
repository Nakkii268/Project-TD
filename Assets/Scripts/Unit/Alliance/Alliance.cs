using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alliance : Character, IDamageable, IHealable, IHasHpBar
{
    [SerializeField] private Vector2 unitPos;
    [SerializeField] private AllianceUnit unit;
    [SerializeField] private Vector2 direction;
    [SerializeField] private bool isDeloyed;

    [SerializeField] private AllianceDirection allianceDirection;

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

    [SerializeField] private AllianceDirectionCircle directionCircle;
    [SerializeField] private AllianceHeathBar heathBar;
    
    public Collider UnitUICollider;
    public int charIndex;

    public event EventHandler OnGetHit;
    public event EventHandler OnUnitRetreat;
    public event EventHandler<float> OnHpChange;
    public float testattack;

    private void Start()
    {
    
        allianceDirection.OnDeloyed += AllianceDirection_OnDeloyed;
       
        LevelManager.instance.OnClickOtherTarget += LevelManager_OnClickOtherTarget;
       
    }

    private void LevelManager_OnClickOtherTarget(object sender, System.EventArgs e)
    {
        if (allianceInfo == null) return;
        if (!isDeloyed)
        {
            Retreat(false);
        }
        if (!allianceInfo.gameObject.activeInHierarchy) return;
        
        if (allianceInfo.IsPointerIn()) return;
        
        
        CameraManager.instance.SetCameraOriginRotation();
        UIHide();
    }

    
    private void AllianceDirection_OnDeloyed(object sender, Vector2 e)
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
       
        
    }
    public void Retreat(bool isRetreat)
    {
        if (isRetreat)
        {
            InGameCharListUI.Instance.ShowRetreatedUnitUI(charIndex);
        }
        isDeloyed = false;
        directionCircle.gameObject.SetActive(false );
        LevelManager.instance.UnHighLightBlockList(allianceAttackRange.AttackRange);
        Block block = GetComponentInParent<Block>();
        block.UnitReTreat();
        
        CameraManager.instance.SetCameraOriginRotation();
        
    }
    public void UIShowOnForcus()
    {
        if (isDeloyed)
        {

            allianceInfo.gameObject.SetActive(true);
            LevelManager.instance.HighLightBlockList(allianceAttackRange.AttackRange, 8);
            UnitUICollider.gameObject.SetActive(true);

        }
    }
    public void UIHide()
    {
        allianceInfo.gameObject.SetActive(false);
        allianceDirection.gameObject.SetActive(false);
        LevelManager.instance.UnHighLightBlockList(allianceAttackRange.AttackRange);
        UnitUICollider.gameObject.SetActive(false);

    }

    public void ReceiveDamaged(float damage,DamageType type)
    {
        if(type == DamageType.MagicDamage)
        {
            
            allianceStat.currentHp -= (damage - damage * (GetReductionValue(allianceStat.Resistance.Value)));

            OnGetHit?.Invoke(this, EventArgs.Empty);
            OnHpChange?.Invoke(this, allianceStat.currentHp/allianceStat.MaxHp.Value);

        }else if(type == DamageType.PhysicDamage)
        {
            allianceStat.currentHp -= (damage - damage * (GetReductionValue(allianceStat.Defense.Value)));

            OnGetHit?.Invoke(this, EventArgs.Empty);
            OnHpChange?.Invoke(this, allianceStat.currentHp / allianceStat.MaxHp.Value);
        }
        else if(type == DamageType.TrueDamage)
        {
            allianceStat.currentHp -= damage;

            OnGetHit?.Invoke(this, EventArgs.Empty);
            OnHpChange?.Invoke(this, allianceStat.currentHp / allianceStat.MaxHp.Value);
            Debug.Log(allianceStat.currentHp);
            Debug.Log(allianceStat.MaxHp.Value);
        }
        if(allianceStat.currentHp < 0)
        {
            allianceStat.currentHp = 0;
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
    public void Use()
    {
        Debug.Log(Stat.Attack.Value);
    }

}
