using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageInfoUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI lifePointText;
    [SerializeField] private TextMeshProUGUI reducedLifePointText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    private void Start()
    {
        LevelManager.instance.LevelLifePointManager.OnLifePointChange += LevelLifePointManager_OnLifePointChange;
        LevelManager.instance.WaveManager.OnEnemyChange += WaveManager_OnEnemyChange;
    }

    private void WaveManager_OnEnemyChange(object sender, WaveEvtarg e)
    {
        enemyCountText.text = e.EnemyCount.ToString() + " / " + e.MaxEnemyCount.ToString();

    }

    private void LevelLifePointManager_OnLifePointChange(object sender, LifePointEvtArg e)
    {
        lifePointText.text = e.Remain.ToString();
        ReduceLifePointTxt(e.Reduced);
    }

    private void ReduceLifePointTxt(float value)
    {
        if (value == 0)
        {
            reducedLifePointText.gameObject.SetActive(false);
        }
        else if (value != 0 && !reducedLifePointText.gameObject.activeSelf)
        {
            reducedLifePointText.gameObject.SetActive(true);

        }
        reducedLifePointText.text = "-" + value.ToString();

    }
    
}
