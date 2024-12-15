using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineManager 
{
    public IState currentState;
    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }
    public void Update()
    {
        currentState?.Update();
    }
    public void FixedUpate()
    {
        currentState?.FixedUpdate();
    }
    public void OnAnimationEnterEvent()
    {
        currentState?.OnAnimationEnterEvent();
    }
    public void OnAnimationExitEvent()
    {
        currentState?.OnAnimationExitEvent();
    }
    public void OnAnimationTransitionEvent()
    {
        currentState?.OnAnimationTransitionEvent();
    }
}
