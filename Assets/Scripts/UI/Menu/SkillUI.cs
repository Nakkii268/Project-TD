using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Image SkillIcon;
    [SerializeField] private TextMeshProUGUI SkillName;
    [SerializeField] private TextMeshProUGUI SkillTpye;
    [SerializeField] private TextMeshProUGUI SkillActive;
    [SerializeField] private TextMeshProUGUI SkillSP;
    [SerializeField] private int SkillIndex;
    public event EventHandler<int> OnSkillSelected;
     public void Init(Skills skill,int SIndex)
    {
        SkillIcon.sprite = skill.Icon;
        SkillName.text = skill.Name;
        SkillTpye.text ="Type"+ skill.skillType.ToString();
        if (skill.skillType == SkillType.Active)
        {
            ActiveSkills askill = (ActiveSkills)skill;
            SkillActive.text = askill.ActiveType.ToString();
            SkillSP.text ="Skill point" +askill.SkillPoint.ToString();
        }
        else
        {
            SkillActive.text = "";
            SkillSP.text = "";

        }
        SkillIndex = SIndex;
        _btn.onClick.AddListener(() =>
        {
            OnSkillSelected?.Invoke(this,SkillIndex); 
        });
    }
}
