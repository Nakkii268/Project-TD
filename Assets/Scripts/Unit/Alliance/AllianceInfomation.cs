using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AllianceInfomation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Alliance unit;
    [SerializeField] private Button RetreatBtn;
    [SerializeField] private Button SkillActiveBtn;
    [SerializeField] private bool isPointerIn;

    

    private void Start()
    {
        RetreatBtn.onClick.AddListener(() =>
        {
            unit.Retreat(true);
            unit.UIHide();
            CameraManager.instance.SetCameraOriginRotation();
        });
        SkillActiveBtn.onClick.AddListener(() => {
            if (unit.AllianceSkill.OnUseSkill.TargetRequire && !unit.AllianceAttack.IsHaveTarget()) { 
                unit.UIHide(); 
                return; 
            }
            unit.UIHide();
            CameraManager.instance.SetCameraOriginRotation();

        });
    }
    private void OnDisable()
    {
        if (gameObject.activeSelf)
        {
            LevelManager.instance.TimeNormal();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerIn = false;


    }

    public bool IsPointerIn()
    {
        return isPointerIn;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
    }
}
