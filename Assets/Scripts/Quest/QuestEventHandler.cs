using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestEventHandler 
{
    public static event Action<MapSO> OnStageClear;
    public static event Action<AllianceUnit> OnUnitLevelup;
    public static void StageClear(MapSO map)
    {
        OnStageClear?.Invoke(map);
    }
    public static void LevelUp(AllianceUnit unit)
    {
        OnUnitLevelup?.Invoke(unit);
    }
}
