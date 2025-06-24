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
                int targetAmount = int.Parse(config.Goals[0]);
               
                if (config.Goals.Count >= 2)
                {
                    string stageid = config.Goals[1];
                    return new ClearStageEvaluate(targetAmount, stageid);

                }
                return new ClearStageEvaluate(targetAmount);
            case "KillEnemy":
              
                int amount = int.Parse(config.Goals[0]);
                return new KillEnemyEvaluate(amount);
            case "LevelUp":
                int level = int.Parse(config.Goals[0]);
                if (config.Goals.Count >= 2)
                {
                   string unitId = config.Goals[1];
                    return new LevelUpEvaluate(level, unitId);

                }
                return new LevelUpEvaluate(level);
            default: return null;
        }
    }
    
}
