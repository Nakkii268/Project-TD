using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterClass/Defender", fileName = "Defender_")]

public class CharacterDefender : CharacterClass
{
    public DefenderBranch SubClassName;
    public StatusEffect BranchPassive;
}
