using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterClass/Vanguard", fileName = "Vanguard_")]

public class CharacterVanguard : CharacterClass
{
    public VanguardBranch SubClassName;
    public StatusEffect BranchPassive;
}
