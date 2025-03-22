using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CharacterClass/Sniper", fileName = "Sniper_")]


public class CharacterSniper : CharacterClass
{
    public SniperBranch SubClassName;
    public StatusEffect BranchPassive;
}
