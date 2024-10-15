using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameCharUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event EventHandler OnCharSelect;
    public event EventHandler<CharacterData> OnCharDrop;

    public AllianceUnit unit;
    public GameObject Prefab;
    public GameObject toPrefab;
    public int num;
    public Canvas Canvas;

    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 2);
        Prefab.GetComponent<Image>().sprite = unit.unitSprite;
        Prefab.GetComponent<RectTransform>().position = Input.mousePosition;
        Prefab.gameObject.SetActive(true);
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        Prefab.GetComponent<RectTransform>().anchoredPosition += eventData.delta/Canvas.scaleFactor;
        OnCharSelect?.Invoke(this,EventArgs.Empty);
        foreach(var block in LevelManager.instance.ValidBlock(unit.GetAllianceType()))
        {
            block.GetComponent<Block>().HighLightBlock();
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Prefab.gameObject.SetActive(false);
        gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
        OnCharDrop?.Invoke(this, new CharacterData(toPrefab,num,unit));
       

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 2);
    }
}
public class CharacterData
{
    public GameObject character;
    public int num;
    public AllianceUnit unit;

    public CharacterData(GameObject c,int n, AllianceUnit u) { character = c; num = n;unit = u; }   
}
