using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSingle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI QuestDesc;
    [SerializeField] private TextMeshProUGUI QuestProgressTxt;
    [SerializeField] private Slider QuestProgress;
    [SerializeField] private Button ClaimBtn;
    public void Inititalized(ActiveQuest quest)
    {
        QuestDesc.text = quest.QuestDescription;
        QuestProgressTxt.text = quest.GetProgress();
        QuestProgress.value = quest.GetProgressPercent();
        ClaimBtn.onClick.AddListener(() => { quest.QuestCompleted(); });
        if (quest.IsCompleted && quest.CurrentState == QuestState.OnGoing)
        {
            ClaimBtn.interactable = true;
        }
        else if(!quest.IsCompleted || quest.CurrentState==QuestState.Locked || quest.CurrentState == QuestState.Completed)
        
        {
            ClaimBtn.interactable= false;
        }
    }
}
