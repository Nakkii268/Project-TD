using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass : ScriptableObject
{
    public Class ClassName;
    public Sprite ClassIcon;
    public StatusEffect ClassPassive;
    public LevelUpSO ClassLevelUpData;
    public virtual void ApplyBuff(GameObject target)
    {

    }
}
