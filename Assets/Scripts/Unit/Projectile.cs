using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damaged;
    [SerializeField] private DamageType damageType;
    [SerializeField] private GameObject target;
    [SerializeField] private UnitTarget targetUnit;
    [SerializeField] private float Speed =5f;
    [SerializeField] private Transform source;
    [SerializeField] private SpriteRenderer projectileVisual;
    [SerializeField] private ParticleSystem hitParticle;



    private void Update()
    {
        MoveToTarget();
        RotateToTarget();
        
    }
    public void SetInfomation(float dmg, DamageType type,UnitTarget ut, GameObject tg,Transform s,Sprite visual,ParticleSystem hit)
    {
        damaged = dmg;  
        damageType = type;
        targetUnit = ut;
        target = tg;
        source = s;
        projectileVisual.sprite = visual;
        hitParticle = hit;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform == target.transform)
        {
            DamagedTarget();
            gameObject.SetActive(false);
        }
    }

    private void DamagedTarget()
    {
        if (targetUnit == UnitTarget.Enemy)
        { 
                target.GetComponentInParent<IDamageable>().ReceiveDamaged(damaged, damageType); 

        }
        else if (targetUnit == UnitTarget.Alliance)
        {
            target.GetComponentInParent<IHealable>().Heal(damaged);
            
        }
        LevelManager.instance.ParticleManager.HitParticle(target,hitParticle);
    }
    private void MoveToTarget()
    {
        if (!target)
        {
            gameObject.SetActive(false);
        }
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
        
        
    }
    private void RotateToTarget()
    {
       Vector2 dir = target.transform.position - source.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle );   
        
      
    }
   
}
