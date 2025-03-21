using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterClass/Sniper", fileName = "Sniper_")]

public class CharacterSniper : MonoBehaviour
{
    public SniperBranch SubClassName;
    public StatusEffect BranchPassive;
}
