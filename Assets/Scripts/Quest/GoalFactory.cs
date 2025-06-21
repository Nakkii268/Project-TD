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
                string stageid = config.Goals["TargetStage"];
                return new ClearStageEvaluate(stageid);
            case "KillEnemy":
                int amount = int.Parse(config.Goals["TargetAmount"]);
                return new KillEnemyEvaluate(amount);

            default: return null;
        }
    }
}
