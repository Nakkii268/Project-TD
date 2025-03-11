using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator animator;
    public void PlayIdleAnim()
    {
        animator.Play("Idle");
    }
    public void PlayMoveAnim()
    {
        animator.Play("Moving");
    }
    public void PlayAttackAnim()
    {
        animator.Play("Attack");
    }
    public void PlayGetHitAnim()
    {
        animator.Play("GetHit");
    }
    public void PlayDeadAnim()
    {
        animator.Play("Dead");
    }
    public void PlayDisableAnim()
    {
        animator.Play("Disable");
    }

    public void OnAnimationEnterEvent()
    {
        enemy.EnemySMManager.OnAnimationEnterEvent();
    }
    public void OnAnimationTransitionEvent()
    {
        enemy.EnemySMManager.OnAnimationTransitionEvent();
    }
    public void OnAnimationExitEvent()
    {
        enemy.EnemySMManager.OnAnimationExitEvent();
    }
}
