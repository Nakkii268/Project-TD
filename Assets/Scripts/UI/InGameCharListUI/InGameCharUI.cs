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

    [SerializeField]private AllianceUnit SlotUnit;
    [SerializeField] private GameObject DragPrefab;
    [SerializeField] private GameObject SpawnPrefab;
    [SerializeField] private int SlotIndex;
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
        UnitCostTxt.text = SlotUnit.UnitDp.ToString();
        ClassIcon.sprite = SlotUnit.UnitClass.ClassIcon;
        Potrait.sprite = SlotUnit.unitPotrait;
        Canvas= UIManager.Instance.GetComponent<Canvas>();
    }
    public void Init(AllianceUnit unit, int inx, GameObject drag) { 
        SlotUnit = unit;
        SlotIndex = inx;
        Potrait.sprite = unit.unitPotrait;
        ClassIcon.sprite = unit.UnitClass.ClassIcon;
        UnitCostTxt.text = unit.UnitDp.ToString();
        DragPrefab = drag;
        SpawnPrefab = unit.UnitPrefab;
        UIManager.Instance.OpenUI<MenuUI>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if ((levelManager.GetLevelDPManager().GetCurrentDp() < SlotUnit.UnitDp) 
            || !countDownUI.canDeloy 
            || !levelManager.GetLevelDPManager().ReachDeployLimit()) return;

        rectTransform.localPosition += new Vector3(0, .5f, 0);
        DragPrefab.GetComponent<Image>().sprite = SlotUnit.unitDragSprite;
        DragPrefab.GetComponent<RectTransform>().position = Input.mousePosition;
        DragPrefab.gameObject.SetActive(true);
        levelManager.TimeSlow();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if ((levelManager.GetLevelDPManager().GetCurrentDp() < SlotUnit.UnitDp) 
            || !countDownUI.canDeloy 
            ||  !levelManager.GetLevelDPManager().ReachDeployLimit()) return;

        DragPrefab.GetComponent<RectTransform>().anchoredPosition += eventData.delta/Canvas.scaleFactor;
        OnCharSelect?.Invoke(this,EventArgs.Empty);
        foreach(var block in levelManager.MapManager.ValidBlock(SlotUnit.GetAllianceType()))
        {
            block.GetComponent<Block>().HighLightBlock(7);
        }
        levelManager.TimeSlow();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragPrefab.gameObject.SetActive(false);
        rectTransform.localScale = Vector3.one;
        OnCharDrop?.Invoke(this, new CharacterData(SpawnPrefab,SlotUnit,SlotIndex));
        foreach (var block in levelManager.MapManager.ValidBlock(SlotUnit.GetAllianceType()))
        {
            block.GetComponent<Block>().UnHighLightBlock();
        }
        levelManager.TimeNormal();

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
        countDownUI.Inittialize(SlotUnit.RedeployTime);
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
