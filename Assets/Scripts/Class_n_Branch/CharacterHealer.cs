using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterClass/Healer", fileName = "Healer_")]

public class CharacterHealer : CharacterClass
{
    public HealerBranch SubClassName;
    public StatusEffect BranchPassive;
}
