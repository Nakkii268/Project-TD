using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceVisual : MonoBehaviour
{
    [SerializeField] private Alliance alliance;
    [SerializeField] private Animator animator;



    //!!!! Need to handle visual direction when deploy



    //for test
    public void PlayIdleAnim()
    {
        animator.Play("test");
    }
    public void StopIdleAnim()
    {
        animator.SetBool("Idle",false);
        
    }
    public void PlayAttackAnim()
    {

        animator.Play("testAttack");

    }
    public void StopAttackAnim()
    {
        animator.SetBool("Attack", false);

    }
    public void PlayGetHitkAnim()
    {
        animator.Play("Get Hit");
    }
    public void PlayDisableAnim()
    {
        animator.Play("Disable");
    }
    public void PlayDeadAnim()
    {
        animator.SetBool("Dead", true);
    }

    // basicaly anim name will be charname + action eg: char1 idle
    public void PlayAnimation(string name)
    {
        animator.SetBool(name,true);    
    }

    public void StopAnimation(string name) 
    { 
        animator.SetBool(name ,false);
    }

    //

    public void AnimationEnterEvent()
    {
        alliance.StateMachine.OnAnimationEnterEvent();
    }

    public void AnimationTransitionEvent()
    {
        alliance.StateMachine.OnAnimationTransitionEvent();
    }
    public void AnimationExitEvent()
    {
        alliance.StateMachine.OnAnimationExitEvent();
    }
}
