using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private Image CountDownImg;
    [SerializeField] private TextMeshProUGUI CountDowntext;
    [SerializeField] public float CountDownTime;
    [SerializeField] public float CountDownTimeLimit;
    [SerializeField] public bool canDeloy;

    public void Inittialize(float time)
    {
        CountDownTimeLimit = time;
        CountDownTime = CountDownTimeLimit;
        CountDownImg.fillAmount = 1;
        CountDowntext.text = CountDownTime.ToString();
        canDeloy = false;
    }

    public void Update()
    {
        if (CountDownTime >=0)
        {
            CountDownTime -= Time.deltaTime;
            CountDownImg.fillAmount  =  CountDownTime/CountDownTimeLimit;
            CountDowntext.text = Math.Round(CountDownTime, 2).ToString();
        }
        else
        {
            canDeloy = true;
            gameObject.SetActive(false);
        }
    }


}
