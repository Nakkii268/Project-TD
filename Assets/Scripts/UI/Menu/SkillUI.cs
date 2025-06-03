using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Image SkillIcon;
    [SerializeField] private TextMeshProUGUI SkillName;
    [SerializeField] private TextMeshProUGUI SkillSP;

    public void Init(Skills skill)
    {
        SkillIcon.sprite = skill.Icon;
        SkillName.text = skill.Name;
        //SkillSP.text = skill.skill
    }
}
