using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class AllianceSkill : MonoBehaviour
{
    
    public Skills OnUseSkill;
    public Alliance alliance;
    public List<GameObject> target;
    [SerializeField] private float startSkillPoint;
    [SerializeField]private float curSkillPoint;
    [SerializeField] private Button SkillActiveBtn;
    [SerializeField] private bool _isSkillDuration;
    public bool CanAttackInDuration;
    public bool IsSkillDuration {  get { return _isSkillDuration; } }
    public event EventHandler<float> OnSpChange;
    public event EventHandler<float> OnSkillActive;
    public event EventHandler OnSkillEnd;
    private void Start()
    {
        
        alliance.GetAllianceUnit().ApplyClassBuff(alliance.gameObject); //maybe adding status effect or passive idk

        if (OnUseSkill.skillType == SkillType.Active)
        {
            ActiveSkills skill = (ActiveSkills)OnUseSkill;
            curSkillPoint = startSkillPoint;
            if (skill.ActiveType == SkillActiveType.ManualUse)
            {
                SkillActiveBtn.onClick.AddListener(() =>
                {
                    SkillUsing();
                });
            }
           
            DisableSkillBtn();

            if (skill.ChargeType == ChargeType.Defensive)
            {
                alliance.OnGetHit += Alliance_OnGetHit; ;
            }
            else if (skill.ChargeType == ChargeType.Offensive)
            {
                alliance.GetComponentInChildren<IAttackPerform>().OnAttackPerform += AllianceSkill_OnAttackPerform;
            }
            else if (skill.ChargeType == ChargeType.Auto)
            {
                InvokeRepeating("AutoRegenSkillPoint", 1f, 1f);
            }
            CanAttackInDuration = skill.CanAttack;
        }
        else if(OnUseSkill.skillType == SkillType.Passive)
        {
            OnUseSkill.SkillActivate(this, target);
            
            DisableSkillBtn();


        }
        

    }

    private void AllianceSkill_OnAttackPerform(object sender, List<GameObject> e)
    {
        SkillPointRecover(1);

    }

    private void OnDisable()
    {
        CancelInvoke("AutoRegenSkillPoint");
    }
    
    private void Alliance_OnGetHit(object sender, System.EventArgs e)
    {
        SkillPointRecover(1);
    }

    

    public void SkillUsing()
    {
        if (!IsFullSkillPoint()) return;
        if(OnUseSkill.TargetRequire && target==null) return;
        OnUseSkill.SkillActivate(this, target);

        curSkillPoint = 0;

        OnSpChange?.Invoke(this, 0);

        OnSkillActive?.Invoke(this, GetSkillDuration());

        StartCoroutine(SkillActiveDurtation());
        DisableSkillBtn();
    }
    private void SkillPointRecover(float amout)
    {
        if (_isSkillDuration) return;

        ActiveSkills skill = (ActiveSkills)OnUseSkill;

        if (curSkillPoint >= skill.SkillPoint) return;

        curSkillPoint += amout;
        OnSpChange?.Invoke(this,curSkillPoint/skill.SkillPoint);
        
        if (IsFullSkillPoint()){
            if (skill.ActiveType == SkillActiveType.AutoUse)
            {
                SkillUsing();
            }
            else
            {
                EnableSkillBtn();
            }
        }
    }
    private bool IsFullSkillPoint()
    {
        ActiveSkills skill = (ActiveSkills)OnUseSkill;

        return curSkillPoint >= skill.SkillPoint;
    }

    private float GetSkillDuration()
    {
        ActiveSkills skill = (ActiveSkills)OnUseSkill;

        return skill.SkillDuration;
    }
    private void DisableSkillBtn()
    {
        if (OnUseSkill.skillType == SkillType.Passive || !IsFullSkillPoint())
        {
            SkillActiveBtn.interactable = false;
        }
    }
    private void EnableSkillBtn()
    {
        
            SkillActiveBtn.interactable = true;
        
    }
    private void AutoRegenSkillPoint()
    {
        SkillPointRecover(1);

    }
    private IEnumerator SkillActiveDurtation()
    {
        _isSkillDuration = true;
        ActiveSkills skill = (ActiveSkills)OnUseSkill;
        yield return new WaitForSeconds(skill.SkillDuration);
        OnSkillEnd?.Invoke(this, EventArgs.Empty);
        _isSkillDuration = false;
    }
}
