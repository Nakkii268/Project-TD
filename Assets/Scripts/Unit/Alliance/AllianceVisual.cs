using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AllianceVisual : MonoBehaviour
{
    [SerializeField] private Alliance alliance;
    [SerializeField] private Animator animator;
    [SerializeField] private SortingGroup sortingGroup;
    [SerializeField] private ParticleSystem attackVFX;
    [SerializeField] private float SpeedScale=1; //speed multiply for attack animation


    private void Start()
    {
        alliance.StatusEffectHolder.OnEfectGain += StatusEffectHolder_OnEfectGain;
        CalSpeedScale(alliance.Stat.AttackInterval.Value);
    }

    private void StatusEffectHolder_OnEfectGain(object sender, System.EventArgs e)
    {
        SpeedScale = CalSpeedScale(alliance.Stat.AttackInterval.Value);
    }

    public void SetSortingOrder(float value)
    {
        sortingGroup.sortingOrder = Mathf.RoundToInt(20 - value);
    }
    
    public void RotateToTarget(GameObject target)
    {
        Vector2 dir = target.transform.position - transform.position;
        if (Vector2.Angle(dir, new Vector2(-1, 0)) < Vector2.Angle(dir, new Vector2(1, 0)) ){
            // transform.rotation = Quaternion.Euler(30, 180, 0);
            transform.localScale = new Vector3(-1, 1, 1);


        }
        else
        {
            // transform.rotation = Quaternion.Euler(-30, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);


        }
    }
    public void RotateToDirection(Vector2 direction)
    {
        if(direction == new Vector2(-1, 0))
        {
            //transform.rotation = Quaternion.Euler(30, 180, 0);
            transform.localScale = new Vector3(-1,1,1);
        }else if(direction == new Vector2(1, 0))
        {
            // transform.rotation = Quaternion.Euler(-30, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);


        }
    }
    
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
        //var mainVFX = attackVFX.main;
        //mainVFX.simulationSpeed = 10;

        animator.SetFloat("speed", SpeedScale);
        animator.Play("attack");

    }
   
  
    public void PlayDisableAnim()
    {
        animator.Play("disable");
    }
    public void PlayDeadAnim()
    {
        animator.Play("dead");
    }

    public void PlaySkill1Anim()
    {
        animator.Play("skill1");
    }
    public void PlaySkill2Anim()
    {
        animator.Play("skill2");
    }
    public void PlaySkill3Anim()
    {
        animator.Play("skill3");
    }
    //

    private float CalSpeedScale(float atks)
    {
        if (atks >= .5f) return 1;
        return  Mathf.CeilToInt(0.5f / atks);
    }
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
