using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class UnitSelectionUI : UICanvas
{
    [SerializeField] private  List<UnitSelectionSingle> _singles;
    private Dictionary<int,UnitSelectionSingle> _child = new Dictionary<int, UnitSelectionSingle>();
    [SerializeField] private  List<AllianceUnit> _units;
    [SerializeField] private UnitSelectionSingle prefab;
    [SerializeField] private Transform container;
    [SerializeField] private Button _backBtn;
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _confirmBtn;
    [SerializeField] private Button _cancelBtn;

    [Header("StatInfo")]
    
    [SerializeField] private TextMeshProUGUI _attackTxt;
    [SerializeField] private TextMeshProUGUI _healthTxt;
    [SerializeField] private TextMeshProUGUI _defTxt;
    [SerializeField] private TextMeshProUGUI _resTxt;
    [SerializeField] private TextMeshProUGUI _asTxt;
    [SerializeField] private TextMeshProUGUI _blockTxt;
    [SerializeField] private TextMeshProUGUI _redeployTxt;
    [SerializeField] private TextMeshProUGUI _costTxt;

    [Header("Info")]

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _class;
    [SerializeField] private Image _range;
    public event EventHandler<AllianceUnit> OnUnitConfirm;
    [SerializeField] private AllianceUnit _currentSelectUnit;
    [SerializeField] private int _currentSelectIndex;

    private void Start()
    {

        _backBtn.onClick.AddListener(() =>
        {
            OnUnitConfirm?.Invoke(this, null);
            UIManager.Instance.Close<UnitSelectionUI>(0);
        });

        _homeBtn.onClick.AddListener(() =>
        {
            OnUnitConfirm?.Invoke(this, null);
            UIManager.Instance.ToHomeMenu();


        });
        _confirmBtn.onClick.AddListener(() =>
        {
            OnUnitConfirm?.Invoke(this, _currentSelectUnit);
            UIManager.Instance.Close<UnitSelectionUI>(0);

        });

        _cancelBtn.onClick.AddListener(() =>
        {
            OnUnitConfirm?.Invoke(this, _currentSelectUnit);
            UIManager.Instance.Close<UnitSelectionUI>(0);



        });
    }
    public override void SetUp()
    {
        Initialized();
    }
    private void Initialized()
    {
        for (int i = 0; i < _units.Count; i++)
        {

            UnitSelectionSingle single = Instantiate(prefab, container);
            single.OnUnitSelected += Single_OnUnitSelected;
            _singles.Add(single);
            single.Initialized(_units[i], i);
            single.gameObject.SetActive(true);
            _child.Add(i, single);


        }
    }

    private void Single_OnUnitSelected(object sender, UnitSelectArg e)
    {
        _child[_currentSelectIndex].UnSelected();
        UpdateInfomation(e.Unit);
        _currentSelectUnit = e.Unit;
        _currentSelectIndex = e.Index;
        

    }

    private void UpdateInfomation(AllianceUnit e)
    {
        if (e == null) return;
        _name.text = e.Name;
        _class.sprite = e.UnitClass.ClassIcon;
        _range.sprite = e.UnitRangeVisualized;
        _attackTxt.text = e.Attack.ToString();
        _healthTxt.text = e.Heath.ToString();
        _defTxt.text = e.Defense.ToString();
        _resTxt.text = e.Resistance.ToString();
        _asTxt.text = e.AttackInterval.ToString();
        _blockTxt.text = e.Block.ToString();
        _redeployTxt.text = e.RedeployTime.ToString();
        _costTxt.text = e.UnitDp.ToString();
    }
}
