using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator animator;
    public void PlayIdleAnim()
    {
        animator.Play("idle");
    }
    public void PlayMoveAnim()
    {
        animator.Play("moving");
    }
    public void PlayAttackAnim()
    {
        animator.Play("attack");
    }
    public void PlayGetHitAnim()
    {
        animator.Play("getHit");
    }
    public void PlayDeadAnim()
    {
        animator.Play("dead");
    }
    public void PlayDisableAnim()
    {
        animator.Play("disable");
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
