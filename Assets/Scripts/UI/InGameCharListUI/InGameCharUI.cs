using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameCharUI : PointerDetect, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private LevelManager levelManager;
    public event EventHandler OnCharSelect;
    public event EventHandler<CharacterData> OnCharDrop;

    [SerializeField]private AllianceUnit unit;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private GameObject toPrefab;
    [SerializeField] private int indx;
    [SerializeField]private Canvas Canvas;
    [SerializeField] private Image Potrait;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private  CountDownUI countDownUI;
    [SerializeField] private TextMeshProUGUI UnitCostTxt;
    [SerializeField] private Image ClassIcon;
    [SerializeField] private bool isForcused;
    protected override void Start()
    {
        base.Start();
        levelManager = LevelManager.instance;
        rectTransform = GetComponent<RectTransform>();
        UnitCostTxt.text = unit.UnitDp.ToString();
        ClassIcon.sprite = unit.UnitClass.ClassIcon;
        Potrait.sprite = unit.unitPotrait;
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
        levelManager.TimeSlow();
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
        levelManager.TimeSlow();

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
    protected override void PointerClickHandler_OnPointerClick(object sender, EventArgs e)
    {
        if (isPointerIn)
        {
            if (isForcused) return;
            rectTransform.position += new Vector3(0,  50,0);
            isForcused = true;
        }
        else
        {
            if (!isForcused) return;
            rectTransform.position -= new Vector3(0, 50, 0);
            isForcused = false;

        }
    }

    public void InitCountDown()
    {
        countDownUI.gameObject.SetActive(true);
        countDownUI.Inittialize(unit.RedeployTime);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
}
public class CharacterData
{
    public GameObject character;
    
    public AllianceUnit unit;
    public int charIndex;

    public CharacterData(GameObject c, AllianceUnit u,int index) { character = c;unit = u;charIndex = index; }   
}
