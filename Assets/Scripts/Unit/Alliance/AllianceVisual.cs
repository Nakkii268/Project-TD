using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceVisual : MonoBehaviour
{
    [SerializeField] private Alliance alliance;
    [SerializeField] private Animator animator;



    //!!!! Need to handle visual direction when deploy

   

    public void RotateToDirection(Vector2 direction)
    {
        if(direction == new Vector2(-1, 0))
        {
            transform.rotation = Quaternion.Euler(30, 180, 0);
        }else if(direction == new Vector2(1, 0))
        {
            transform.rotation = Quaternion.Euler(-30, 0, 0);

        }
    }
    //for test}
    public void PlayDefaultAnim()
    {
        animator.Play("default");
    }
    public void PlayIdleAnim()
    {
        animator.Play("idle");
    }
  
    public void PlayAttackAnim()
    {

        animator.Play("attack");

    }
  
    public void PlayGetHitkAnim()
    {
        animator.Play("gethit");
    }
    public void PlayDisableAnim()
    {
        animator.Play("disable");
    }
    public void PlayDeadAnim()
    {
        animator.Play("dead");
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
