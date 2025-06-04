using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skill/ComplexSkill/AOE")]
public class AOEComplexSkills : ComplexSkills
{
    public Vector2 Range;
    public LayerMask TargetLayer;
    public bool IsAroundUser;

    public override void SkillActivate(AllianceSkill User, List<GameObject> target = null)
    {
        LevelManager.instance.ParticleManager.SkillParticle(User.gameObject, SkillVFX, User.transform, User.alliance.GetVFXQuaternion());

        User.StartCoroutine(DelayStatus(User, target, EffectDelayTime));
        User.StartCoroutine(DelayDamage(User, target, DamageDelayTime));
    }
    public override void DamageComponent(AllianceSkill User, List<GameObject> target)
    {
        if (skillTarget == SkillTarget.Self)
        {
            //dmg self
            User.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmg, DamageType);


        }


        if (skillTarget == SkillTarget.Enemy)
        {
            //dmg target
            foreach (Collider2D tg in GetTarget(User))
            {
                tg.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmg, DamageType);

            }
        }
    }
    public override void EffectComponent(AllianceSkill User, List<GameObject> target)
    {
        if (subTarget == SkillSubTarget.Enemy)
        {
            //debuff target
            foreach (Collider2D tg in GetTarget(User))
            {
                StatusEffectHolder holder = tg.GetComponentInParent<StatusEffectHolder>();

                for (int i = 0; i < effects.Count; i++)
                {
                    holder.AddStatusEffect(tg.gameObject, effects[i]);
                }
            }
        }

        if (subTarget == SkillSubTarget.Self)
        {
            //buff self
            StatusEffectHolder holder = User.GetComponentInParent<StatusEffectHolder>();

            for (int i = 0; i < effects.Count; i++)
            {
                holder.AddStatusEffect(User.gameObject, effects[i]);
            }
        }
    }
    private Collider2D[] GetTarget(AllianceSkill User)
    {
        float centerx = 0;
        float centery = 0;
        if (IsAroundUser)
        {
            centerx = User.alliance.UnitPos.x;
            centery = User.alliance.UnitPos.y;
        }
        else
        {
            centerx = ((RangeSwap(User.alliance.direction).x / 2 + .5f) * User.alliance.direction).x + (User.alliance.UnitPos.x);
            centery = ((RangeSwap(User.alliance.direction).y / 2 + .5f) * User.alliance.direction).y + (User.alliance.UnitPos.y);
        }
        return Physics2D.OverlapBoxAll(new Vector2(centerx, centery), RangeSwap(User.alliance.direction), 0, TargetLayer, -5, 5);
    }

    private Vector2 RangeSwap(Vector2 dir)
    {
        if (dir.x != 0)
        {
            return Range;
        }
        else
        {
            return new Vector2(Range.y, Range.x);
        }
    }
   
}
