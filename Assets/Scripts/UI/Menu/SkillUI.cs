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
    [SerializeField] private GameObject Selected;
    public event EventHandler<int> OnSkillSelected;
    public void Init(Skills skill, int SIndex, bool isSelected)
    {
        SkillIcon.sprite = skill.Icon;
        SkillName.text = skill.Name;
        SkillTpye.text = "Type" + skill.skillType.ToString();
        if (skill.skillType == SkillType.Active)
        {
            ActiveSkills askill = (ActiveSkills)skill;
            SkillActive.text = askill.ActiveType.ToString();
            SkillSP.text = "Skill point" + askill.SkillPoint.ToString();
        }
        else
        {
            SkillActive.text = "";
            SkillSP.text = "";

        }
        if (isSelected)
        {
            Selected.gameObject.SetActive(true);
        }
        else
        {
            Selected.gameObject.SetActive(false);
        }
        SkillIndex = SIndex;
        _btn.onClick.AddListener(() =>
        {
            OnSkillSelected?.Invoke(this, SkillIndex);
        });

    }

    public void SkillSelected()
    {
        Selected.gameObject.SetActive(true);
        _btn.interactable = false;
    }
    public void SkillDeSelected()
    {
        Selected.gameObject.SetActive(false);
        _btn.interactable = true;
    }
}
