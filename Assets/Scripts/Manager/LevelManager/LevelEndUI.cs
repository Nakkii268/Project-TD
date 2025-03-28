using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndUI : MonoBehaviour
{
    [SerializeField] private GameObject LevelLose;
    [SerializeField] private GameObject LevelWin;
    private void Start()
    {
            LevelWin.SetActive(false);
            LevelLose.SetActive(false);

    }
    public void ActiveUI(EndState state)
    {
        if (state == EndState.Successed)
        {
            LevelWin.SetActive(true);
        }
        else if (state == EndState.Failed)
        {
            LevelLose.SetActive(true);

        }
        else
        {
            LevelWin.SetActive(true);
        }
    }

    private void LevelRating(EndState state) { 
        // successed ==3star
        //  notcomplete ==2start
        //failed = 0star
    }
    private void LevelReward()
    {
        //
    }
}
