using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private bool IsSkillDuration;
    private void Start()
    {
        
        alliance.GetAllianceUnit().ApplyClassBuff(alliance.gameObject); //maybe adding status effect or passive dk
        if (OnUseSkill.skillType == SkillType.Active)
        {
            ActiveSkills skill = (ActiveSkills)OnUseSkill;
            curSkillPoint = startSkillPoint;

            SkillActiveBtn.onClick.AddListener(() => {
                SkillUsing();
            });

            DisableSkillBtn();

            if (skill.ChargeType == ChargeType.Defensive)
            {
                alliance.OnGetHit += Alliance_OnGetHit; ;
            }
            else if (skill.ChargeType == ChargeType.Offensive)
            {
                alliance.AllianceAttack.OnAttackPerform += AllianceAttack_OnAttackPerform;
            }
            else if (skill.ChargeType == ChargeType.Auto)
            {
                InvokeRepeating("AutoRegenSkillPoint", 1f, 1f);
            }
        }else if(OnUseSkill.skillType == SkillType.Passive)
        {
            OnUseSkill.SkillActivate(alliance.gameObject, target);
            
            DisableSkillBtn();


        }


    }

    private void OnDisable()
    {
        CancelInvoke("AutoRegenSkillPoint");
    }
    private void AllianceAttack_OnAttackPerform(object sender, System.EventArgs e)
    {
        SkillPointRecover(1);
    }

    private void Alliance_OnGetHit(object sender, System.EventArgs e)
    {
        SkillPointRecover(1);
    }

    

    public void SkillUsing()
    {
        if (!IsFullSkillPoint()) return;
        OnUseSkill.SkillActivate(alliance.gameObject, target);
        curSkillPoint = 0;
        StartCoroutine(SkillActiveDurtation());
        DisableSkillBtn();
    }
    private void SkillPointRecover(float amout)
    {
        if (IsSkillDuration) return;
        ActiveSkills skill = (ActiveSkills)OnUseSkill;

        if (curSkillPoint >= skill.SkillPoint) return;
        curSkillPoint += amout;
        if (IsFullSkillPoint()){
            EnableSkillBtn();
        }
    }
    private bool IsFullSkillPoint()
    {
        ActiveSkills skill = (ActiveSkills)OnUseSkill;

        return curSkillPoint >= skill.SkillPoint;
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
        IsSkillDuration = true;
        ActiveSkills skill = (ActiveSkills)OnUseSkill;
        yield return new WaitForSeconds(skill.SkillDuration);
        IsSkillDuration = false;
    }
}
