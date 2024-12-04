using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public enum Class
{
    Guard,
    Defender,
    Healer,
    Mage,
    Sniper
}
public enum GuardBranch
{
    Brawler,
    Knight
}
public enum DefenderBranch
{
    Tank,
    Fortress,
    Juggernaut
}
public enum HealerBranch
{
    Medic,
    MultiTarget
}
public enum MageBranch
{
    CoreCaster,
    SpashCaster,
    ChainCaster
}
public enum SniperBranch
{
    Maskman,
    HeavyShooter
}