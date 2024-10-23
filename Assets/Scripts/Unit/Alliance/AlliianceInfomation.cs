using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlliianceInfomation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Alliance unit;
    [SerializeField] private Button RetreatBtn;
    [SerializeField] private Button SkillActiveBtn;
    [SerializeField] private bool isPointerIn;

    

    private void Start()
    {
        RetreatBtn.onClick.AddListener(() =>
        {
            unit.Retreat();
            unit.UIHide();
            CameraManager.instance.SetCameraOriginRotation();
        });
        SkillActiveBtn.onClick.AddListener(() => {
            Debug.Log("---Skill active---");
            unit.UIHide();
            CameraManager.instance.SetCameraOriginRotation();

        });
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
}
