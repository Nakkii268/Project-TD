using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameCharUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private LevelManager levelManager;
    public event EventHandler OnCharSelect;
    public event EventHandler<CharacterData> OnCharDrop;

    [SerializeField]private AllianceUnit unit;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private GameObject toPrefab;
    [SerializeField] private int indx;
   [SerializeField]private Canvas Canvas;
    [SerializeField] private bool isPointerHover;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private  CountDownUI countDownUI;

    private void Start()
    {
        levelManager = LevelManager.instance;
        rectTransform = GetComponent<RectTransform>();
    }
    public void Initialized()
    {
        //toPrefab = unit.UnitPrefab;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if ((levelManager.GetLevelDPManager().GetCurrentDp() < unit.UnitDp) 
            || !countDownUI.canDeloy 
            || !levelManager.GetLevelDPManager().ReachDeployLimit()) return;

        rectTransform.localPosition += new Vector3(0, .5f, 0);
        Prefab.GetComponent<Image>().sprite = unit.unitSprite;
        Prefab.GetComponent<RectTransform>().position = Input.mousePosition;
        Prefab.gameObject.SetActive(true);
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        if ((levelManager.GetLevelDPManager().GetCurrentDp() < unit.UnitDp) 
            || !countDownUI.canDeloy 
            ||  !levelManager.GetLevelDPManager().ReachDeployLimit()) return;

        Prefab.GetComponent<RectTransform>().anchoredPosition += eventData.delta/Canvas.scaleFactor;
        OnCharSelect?.Invoke(this,EventArgs.Empty);
        foreach(var block in levelManager.MapManager.ValidBlock(unit.GetAllianceType()))
        {
            block.GetComponent<Block>().HighLightBlock(7);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Prefab.gameObject.SetActive(false);
        rectTransform.localScale = Vector3.one;
        OnCharDrop?.Invoke(this, new CharacterData(toPrefab,unit,indx));
        foreach (var block in levelManager.MapManager.ValidBlock(unit.GetAllianceType()))
        {
            block.GetComponent<Block>().UnHighLightBlock();
        }
       

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPointerHover) {

            rectTransform.localPosition += new Vector3(0, .5f, 0);
        }
        else
        {
            rectTransform.localScale = Vector3.one;

        }
        //set time scale
    }
    public void InitCountDown()
    {
        countDownUI.gameObject.SetActive(true);
        countDownUI.Inittialize(unit.RedeployTime);
    }
}
public class CharacterData
{
    public GameObject character;
    
    public AllianceUnit unit;
    public int charIndex;

    public CharacterData(GameObject c, AllianceUnit u,int index) { character = c;unit = u;charIndex = index; }   
}
