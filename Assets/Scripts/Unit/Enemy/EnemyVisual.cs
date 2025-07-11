using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator animator;
    [SerializeField] private SortingGroup sortingGroup;


    public void RotateToTarget(Vector3 target)
    {
        Vector2 dir  = CalDirection(target);
       if(dir == new Vector2(1, 0))
       {
            transform.localScale = new Vector3(1,1,1);
       }
       else if (dir == new Vector2(-1, 0))
       {
            transform.localScale = new Vector3(-1, 1, 1);
       }
    }
    private Vector2 CalDirection(Vector3 target)
    {
        float angle = Vector2.Angle(target-transform.position,new Vector2(1,0));
      

        if (angle <= 90)
        {
            return new Vector2(1, 0);
        }else return new Vector2(-1,0);
    }
    public void SetSortingOrder(float value)
    {
        sortingGroup.sortingOrder = Mathf.RoundToInt(20-value);
    }
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
