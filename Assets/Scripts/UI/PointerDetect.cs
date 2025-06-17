using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PointerDetect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isPointerIn;
  
    protected virtual void OnEnable()
    {
        GameManager.Instance.PointerClickHandler.OnPointerClick += PointerClickHandler_OnPointerClick;
    }
    protected virtual void OnDisable()
    {
        GameManager.Instance.PointerClickHandler.OnPointerClick -= PointerClickHandler_OnPointerClick;

    }
    protected virtual void PointerClickHandler_OnPointerClick(object sender, System.EventArgs e)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerIn = false;
    }
}
