using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceSMManager : StateMachineManager
{
    public Alliance Alliance { get; }
    public AllianceIdleState AllianceIdleState { get; }
    public AllianceAttackState AllianceAttackState { get; }
    public AllianceSkillDuarationState AllianceSkillDuarationState { get; }
    public AllianceDisableState AllianceDisableState { get; }
    public AllianceSkillAttackState AllianceSkillAttackState { get; }
    public AllianceDeadState AllianceDeadState { get; }

    public AllianceSMManager (Alliance alliance)
    {
        Alliance = alliance;
        AllianceIdleState = new AllianceIdleState(this);
        AllianceAttackState = new AllianceAttackState(this);
        AllianceSkillDuarationState = new AllianceSkillDuarationState(this);
        AllianceDisableState = new AllianceDisableState(this);
        AllianceDeadState = new AllianceDeadState(this);
        AllianceSkillAttackState = new AllianceSkillAttackState(this);
    }
}
