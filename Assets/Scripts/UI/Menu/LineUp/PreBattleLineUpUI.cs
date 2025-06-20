using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PreBattleLineUpUI : UICanvas
{
    [SerializeField] private MapSO map;
    [SerializeField] private List<LineUpSlot> _slots;
    [SerializeField] private List<LineUpSave> _playerSquad = new List<LineUpSave>();
    [SerializeField] public Dictionary<int, LineUpSave> _tempSquad = new Dictionary<int, LineUpSave>();

    [SerializeField] private Button _backBtn;
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _battleBtn;

    private void Start()
    {
        //get player squad 

        _backBtn.onClick.AddListener(() =>
        {
            _tempSquad.Clear();

            UIManager.Instance.Close<PreBattleLineUpUI>(0);
        });
        _homeBtn.onClick.AddListener(() => {
            _tempSquad.Clear();
            UIManager.Instance.CloseToHome();
            UIManager.Instance.OpenUI<MenuUI>();
        });
        _battleBtn.onClick.AddListener(() =>
        {
            GameManager.Instance._sceneLoader.LoadStage(map.StagePath);
            UIManager.Instance.OpenUI<StageLoadingUI>(map.MapID);
        });

        _tempSquad = _playerSquad.ToDictionary(item => item.Index);

    }


    public override void SetUp(object b)
    {
        Initialized(b as MapSO);
    }
    private void Initialized(MapSO map)
    {
        this.map = map;
        _playerSquad = GameManager.Instance._playerDataManager.PlayerDataSO.PlayerLineUp;
        for (int i = 0; i < _slots.Count; i++)
        {
            _slots[i].OnUnitAssign += LineUpUI_OnUnitAssign;
            _slots[i].Initialized(null, -1);
            _slots[i].IndexAssign(i);
        }
        for (int i = 0; i < _playerSquad.Count; i++)
        {
            _slots[_playerSquad[i].Index].Initialized(_playerSquad[i].Unit, _playerSquad[i].SkillIndex);


        }

    }

    private void LineUpUI_OnUnitAssign(object sender, LineUpSave e)
    {
        Debug.Log("unit asssign");
        if (_tempSquad.ContainsKey(e.Index))
        {
            if (e.Unit == null) //if unit =null => unit remove
            {
                _tempSquad.Remove(e.Index);
                GameManager.Instance._playerDataManager.PlayerDataSO.UpdateLineUp(e.Unit, e.Index, e.SkillIndex);
                GameManager.Instance._playerDataManager.SaveLineUp();

                return;
            }
            //case unit update
            GameManager.Instance._playerDataManager.PlayerDataSO.UpdateLineUp(e.Unit, e.Index, e.SkillIndex);
            GameManager.Instance._playerDataManager.SaveLineUp();

            _tempSquad[e.Index] = e;

            return;
        }
        //case add new unit
        GameManager.Instance._playerDataManager.PlayerDataSO.UpdateLineUp(e.Unit, e.Index, e.SkillIndex);
        GameManager.Instance._playerDataManager.SaveLineUp();

        _tempSquad.Add(e.Index, e);



    }
}
