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
    }

    public virtual void Exit()
    {
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
}
