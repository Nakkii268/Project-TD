using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlliianceInfomation : MonoBehaviour
{
    [SerializeField] private Alliance unit;
    [SerializeField] private Button RetreatBtn;
    [SerializeField] private Button SkillActiveBtn;

    private void Start()
    {
        RetreatBtn.onClick.AddListener(() =>
        {
            unit.Retreat();
        });
        SkillActiveBtn.onClick.AddListener(() => {
            Debug.Log("---Skill active---");
        });
    }
}
