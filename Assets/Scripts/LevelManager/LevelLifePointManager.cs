using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelLifePointManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private int LifePoint;
    [SerializeField] private int ReducedLifePoint;

    [SerializeField] private TextMeshProUGUI lifePointText;
    [SerializeField] private TextMeshProUGUI reducedLifePointText;
    void Start()
    {
        levelManager = LevelManager.instance;
        LifePoint = 10;// get from map SO through levelmanager 
        ReducedLifePoint = 0;
        SetLifePointText();
        reducedLifePointText.gameObject.SetActive(false);

    }

    public void LifePointReduce()
    {
        LifePoint--;
        ReducedLifePoint++;
        reducedLifePointText.gameObject.SetActive(true);
        SetLifePointText();
        if (LifePoint <= 0) { 
            //lose
        }

    }
    
    private void SetLifePointText()
    {
        lifePointText.text = LifePoint.ToString();
        reducedLifePointText.text = "-" + ReducedLifePoint.ToString();

    }
}
