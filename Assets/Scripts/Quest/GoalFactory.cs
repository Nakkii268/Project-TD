using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GoalFactory
{
    public static IQuestEvaluate Create(GoalConfig config)
    {
        switch (config.GoalType)
        {
            case "ClearStage":
                string stageid = config.Goals;
                return new ClearStageEvaluate(stageid);
            case "KillEnemy":
              
                int amount = int.Parse(config.Goals);
                return new KillEnemyEvaluate(amount);
            case "LevelUp":
                int level = int.Parse(config.Goals);
                return new LevelUpEvaluate(level);
            default: return null;
        }
    }
}
