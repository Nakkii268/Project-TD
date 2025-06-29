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
    public event EventHandler<LineUpSave> OnUnitConfirm;
    [SerializeField] private AllianceUnit _currentSelectUnit;
    [SerializeField] private int _currentSelectIndex=-1;

    [SerializeField] private Transform NoInfoUI;
    [Header("Skills")]
    [SerializeField] private List<SkillUI> _skillUI;

    [SerializeField] private int _currentSkillIndex = -1;

    private void Start()
    {

        _backBtn.onClick.AddListener(() =>
        {
           
            UIManager.Instance.Close<UnitSelectionUI>(0);
        });

        _homeBtn.onClick.AddListener(() =>
        {
            
            UIManager.Instance.ToHomeMenu();


        });
        _confirmBtn.onClick.AddListener(() =>
        {
            OnUnitConfirm?.Invoke(this, new LineUpSave(_currentSelectIndex,_currentSelectUnit, _currentSkillIndex));
            UIManager.Instance.Close<UnitSelectionUI>(0);

        });

        _cancelBtn.onClick.AddListener(() =>
        {
            
            UIManager.Instance.Close<UnitSelectionUI>(0);



        });
        for (int i = 0; i < _skillUI.Count; i++)
        {
            _skillUI[i].OnSkillSelected += Skill_OnSkillSelected;
        }
    }
    public override void SetUp(object t)
    {
        if(t is SlotData data)
        {
            Initialized(data.unit, data.index,data.skillIndex);
        }
        
    }
    private void Initialized(AllianceUnit unit,int idx,int skillIndex)
    {
        _units = GameManager.Instance._playerDataManager.PlayerDataSO.OwnedCharacter;
        //handle info part
        if (unit == null) { 
            NoInfoUI.gameObject.SetActive(true); 
        }
        //handle select item part
        for (int i = 0; i < _units.Count; i++)
        {

            UnitSelectionSingle single = Instantiate(prefab, container);
            single.OnUnitSelected += Single_OnUnitSelected;
            _singles.Add(single);
            single.Initialized(_units[i],i,idx,unit);
            single.gameObject.SetActive(true);
            _child.Add(i, single);
           
            HandleSkillUI(skillIndex);
            if (unit == _units[i])
            {
                _currentSelectIndex = i;

            }
        }
    }
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    private void Single_OnUnitSelected(object sender, UnitSelectData e)
    {
        if (_currentSelectUnit != null)
        {

            _child[_currentSelectIndex].UnSelected();
            
        }
        if(e.Unit == null)
        {
            

            UpdateInfomation(null);
        }
        else
        {
            _currentSkillIndex = 0;
            UpdateInfomation(e.Unit);

        }
        _currentSelectUnit = e.Unit;
        _currentSelectIndex = e.Index;
     

    }



    private void UpdateInfomation(AllianceUnit e)
    {
        if (e == null)
        {
            NoInfoUI.gameObject.SetActive(true);
            return;
        }
        NoInfoUI.gameObject.SetActive(false);
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
        for (int i = 0; i < e.UnitSkills.Count; i++)
        {
            if (_currentSkillIndex == i)
            {
                _skillUI[i].Init(e.UnitSkills[i], i,true);
            }
            else
            {
                _skillUI[i].Init(e.UnitSkills[i], i, false);

            }
        }
        
    }

    private void Skill_OnSkillSelected(object sender, int e)
    {
        HandleSkillUI(e);
         
    }
    private void HandleSkillUI(int e)
    {
        _currentSkillIndex = e;
        for (int i = 0; i < _skillUI.Count; i++)
        {
            if (i == e)
            {
                _skillUI[i].SkillSelected();
            }
            else
            {
                _skillUI[i].SkillDeSelected();

            }
        }
    }
}
