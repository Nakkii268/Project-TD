using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="UnitRarity")]
public class UnitRarity : ScriptableObject
{
    public int Star;
    public int MaxLimitBreak;
    public List<int> LevelCap;
    public Sprite RarityIcon;
}

