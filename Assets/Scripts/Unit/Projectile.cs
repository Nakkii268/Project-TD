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
    private void Start()
    {
        RotateToTarget();
    }
    private void Update()
    {
        
    }
    public void SetInfomation(float dmg, DamageType type,UnitTarget ut, GameObject tg)
    {
        damaged = dmg;  
        damageType = type;
        targetUnit = ut;
        target = tg;
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
    }
    private void RotateToTarget()
    {
        float angle = Vector2.Angle(target.transform.position- transform.position, Vector2.right);
       transform.rotation= Quaternion.Euler(0f, 0f,angle);
        //may be reconsider to use lookat
    }
}
