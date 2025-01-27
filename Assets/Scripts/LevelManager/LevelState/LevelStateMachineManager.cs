using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateMachineManager : StateMachineManager
{
    public LevelManager _levelManager;
    public LevelPrepareState LevelPrepareState { get; }
    public LevelGameState LevelGameState { get; }
    public LevelPauseState  LevelPauseState { get; }
    public LevelEndState LevelEndState { get; }

    public LevelStateMachineManager (LevelManager lm)
    {
        _levelManager = lm;
        LevelPrepareState = new LevelPrepareState(this);
        LevelGameState = new LevelGameState(this);
        LevelPauseState = new LevelPauseState(this);
        LevelEndState = new LevelEndState(this);
    }
    
}
