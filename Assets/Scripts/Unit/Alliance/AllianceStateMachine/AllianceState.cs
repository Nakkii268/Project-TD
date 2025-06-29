using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceState : IState
{
    public AllianceSMManager AllianceSMManager;

    public AllianceState (AllianceSMManager allianceSMManager)
    {
        AllianceSMManager = allianceSMManager;  
    }
    public virtual void Enter()
    {
        Debug.Log(this);
        AddCallBack();
       
    }

    public virtual void Exit()
    {
        RemoveCallBack();
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void OnAnimationEnterEvent()
    {
    }

    public virtual void OnAnimationExitEvent()
    {
    }

    public virtual void OnAnimationTransitionEvent()
    {
    }

    public virtual void Update()
    {
    }

    protected virtual void AddCallBack()
    {
       
        AllianceSMManager.Alliance.AllianceSkill.OnSkillActive += AllianceSkill_OnSkillActive;
        AllianceSMManager.Alliance.OnUnitDead += Alliance_OnUnitDead;
        AllianceSMManager.Alliance.StatusEffectHolder.OnGetDisable += Alliance_OnGetDisable;
       
    }

    private void Alliance_OnGetDisable(object sender, System.EventArgs e)
    {
        AllianceSMManager.ChangeState(AllianceSMManager.AllianceDisableState);
    }

    protected virtual void RemoveCallBack()
    {
        
        AllianceSMManager.Alliance.AllianceSkill.OnSkillActive -= AllianceSkill_OnSkillActive;
        AllianceSMManager.Alliance.OnUnitDead -= Alliance_OnUnitDead;
    }
    private void Alliance_OnUnitDead(object sender, System.EventArgs e)
    {
        
        AllianceSMManager.ChangeState(AllianceSMManager.AllianceDeadState);
    }

    private void AllianceSkill_OnSkillActive(object sender, float e)
    {
        AllianceSMManager.ChangeState(AllianceSMManager.AllianceSkillDuarationState);

    }

    

    
}
