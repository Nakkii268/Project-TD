using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllianceSPBar : MonoBehaviour
{
    [SerializeField] private Alliance alliance;
    [SerializeField] private Image SpBar;
    [SerializeField] private Image DurationBar;

    private void Start()
    {
        alliance.AllianceSkill.OnSpChange += AllianceSkill_OnSpChange;
        alliance.AllianceSkill.OnSkillActive += AllianceSkill_OnSkillActive;
        SpBar.fillAmount = 0;
        DurationBar.fillAmount = 1;
        DurationBar.gameObject.SetActive(false);
        
    }

    private void AllianceSkill_OnSkillActive(object sender, float e)
    {
        SwichtBar(false);
        if (e > 99) return; //if skill duration is unlimited then no need to countdown
        StartCoroutine(SkillDurationBar(e));
    }

    private void AllianceSkill_OnSpChange(object sender, float e)
    {
        SpBar.fillAmount = e;

    }

    private IEnumerator SkillDurationBar(float duration)
    {
        float curDuration = duration;
        while (curDuration > 0) { 
            curDuration -= Time.deltaTime;
            DurationBar.fillAmount = curDuration/duration;
            yield return null;
        }
        SwichtBar(true);


    }
    private void SwichtBar(bool isSp)
    {
        SpBar.gameObject.SetActive(isSp);
        DurationBar.gameObject.SetActive(!isSp);
    }
}
