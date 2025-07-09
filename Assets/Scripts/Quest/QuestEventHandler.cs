using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestEventHandler 
{
    public static event Action<MapSO> OnStageClear;
    public static event Action<AllianceUnit> OnUnitLevelup;
    public static event Action<int> DailyPointAcquire;
    public static event Action<int> WeeklyPointAcquire;
    public static event Action<int> OnConsumeStamina;
    public static event Action<int> OnConsumeGold;
    public static event Action<int> OnConsumeDiamond;
    public static void StageClear(MapSO map)
    {
        OnStageClear?.Invoke(map);
    }
    public static void LevelUp(AllianceUnit unit)
    {
        OnUnitLevelup?.Invoke(unit);
    }
    public static void DailyQuestCompleted(int point)
    {
        DailyPointAcquire?.Invoke(point);
    }
    public static void WeeklyQuestCompleted(int point)
    {
        WeeklyPointAcquire?.Invoke(point);
    }
    public static void StaminaConsume(int anount)
    {
        OnConsumeStamina?.Invoke(anount);
    }
    public static void GoldConsume(int anount)
    {
        OnConsumeGold?.Invoke(anount);
    }
    public static void DiamondConsume(int anount)
    {
        OnConsumeDiamond?.Invoke(anount);
    }

}
