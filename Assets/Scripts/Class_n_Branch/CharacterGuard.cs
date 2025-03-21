using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterClass/Guard", fileName = "Guard_")]

public class CharacterGuard : CharacterClass
{
    public GuardBranch SubClassName;
    public StatusEffect BranchPassive;
}
