using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSingle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI QuestDesc;
    [SerializeField] private TextMeshProUGUI BtnTxt;
    [SerializeField] private Slider QuestProgress;
    [SerializeField] private Button ClaimBtn;
    public event Action OnClaim;
    public void Inititalized(ActiveQuest quest)
    {
        QuestProgress.interactable=false;
        QuestDesc.text = quest.QuestDescription;
     
        QuestProgress.value = quest.GetProgressPercent();
        ClaimBtn.onClick.AddListener(() => {
            quest.QuestCompleted(); 
            OnClaim?.Invoke();
        });
        if (quest.CurrentState == QuestState.OnGoing)
        {
            ClaimBtn.interactable = quest.IsCompleted;
            BtnTxt.text = "CLAIM";

        }
        else if( quest.CurrentState==QuestState.Locked )
        
        {
            ClaimBtn.interactable= false;
            BtnTxt.text = "LOCKED";
            QuestProgress.value = 0;

        }
        else if(quest.CurrentState == QuestState.Completed)
        {
            ClaimBtn.interactable = false;
            BtnTxt.text = "COMPLETED";
            QuestProgress.value = 1;

        }
    }
}
