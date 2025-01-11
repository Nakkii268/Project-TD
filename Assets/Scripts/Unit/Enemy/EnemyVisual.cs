using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    
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
