using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;

public class EnemyAttackCollider : MonoBehaviour
{
    [SerializeField] private Enemy Enemy;
    [SerializeField] private float attackRange;
    [SerializeField] private CircleCollider2D attackCollider;
    [SerializeField] private LayerMask targetLayer;
    public event EventHandler<GameObject> OnTargetIn;
    public event EventHandler<GameObject> OnTargetOut;

    private void Start()
    {
        targetLayer = Enemy.Unit.targetlayer;
        attackCollider.radius = Enemy.Unit.AttackRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((targetLayer.value & (1 << collision.gameObject.layer)) == 0) return;


        if (collision.gameObject.CompareTag("Alliance"))
        {
            OnTargetIn?.Invoke(this, collision.gameObject);


        }
    }





    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((targetLayer.value & (1 << collision.gameObject.layer)) == 0) return;
        
            if (collision.gameObject.CompareTag("Alliance"))
            {
                OnTargetOut?.Invoke(this, collision.gameObject);



            }
    }
    
}
