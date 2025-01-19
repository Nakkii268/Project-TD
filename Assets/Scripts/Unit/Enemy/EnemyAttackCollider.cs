using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
   [SerializeField] private Enemy Enemy;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask targetLayer;

    public GameObject DetectEnenmy()//may be can change return type to list for future
    {
        Collider2D hit;
        hit = Physics2D.OverlapCircle(transform.position, Enemy.Stat.AttackRange,targetLayer,.5f);
        return hit.gameObject;
        
    }
}
