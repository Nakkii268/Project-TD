using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float damaged;
    [SerializeField] protected DamageType damageType;
    [SerializeField] protected GameObject target;
    [SerializeField] protected UnitTarget targetUnit;
    [SerializeField] protected float Speed =5f;
    [SerializeField] protected Transform source;
    [SerializeField] protected SpriteRenderer projectileVisual;
    [SerializeField] protected ParticleSystem hitParticle;



    protected virtual void Update()
    {
        if (!target)
        {
            gameObject.SetActive(false);
            return;
        }
        MoveToTarget();
        RotateToTarget();
        
    }
    public virtual void SetInfomation(float dmg, DamageType type,UnitTarget ut, GameObject tg,Transform s,Sprite visual,ParticleSystem hit)
    {   
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform == target.transform)
        {
            DamagedTarget();
            gameObject.SetActive(false);
        }
    }

    protected virtual void DamagedTarget()
    {
        
    }
    protected virtual void MoveToTarget()
    {
        
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
        
        
    }
    protected virtual void RotateToTarget()
    {
       Vector2 dir = target.transform.position - source.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle );   
        
    }
   
}
