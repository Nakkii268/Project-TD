using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="StatusEffect/OnHitEffect/AOEattack")]
public class SpashAttackBuff : OnHitStatusEffect    
{
    public float Radius;
    public float Scale;
    public DamageType DamageType;

    public override void OnApply(GameObject holder, GameObject target)
    {
        Alliance alliance = holder.GetComponentInParent<Alliance>();
        Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
        float dmg = alliance.Stat.Attack.Value * Scale;
        LayerMask layer = alliance.AllienceAttackCollider.GetEnemyLayer();
        Collider2D[] hits = Physics2D.OverlapCircleAll(targetPos, Radius, layer,0,1);
        foreach(Collider2D hit in hits)
        {
            hit.GetComponent<Enemy>().ReceiveDamaged(dmg,DamageType);
        }

        
    }

}
