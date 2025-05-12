using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    public static MenuUIManager Instance;
    [SerializeField] private StageSelectUI _stageUI;
    [SerializeField] private UnitListUI _unitListUI;
    [SerializeField] private CharacterInfoUI _characterInfoUI;
    [SerializeField] private UnitLevelUpUI _levelUpUI;
    [SerializeField] private UnitLimitBreakUI _limitBreakUI;

    public StageSelectUI StageSelectUI { get { return _stageUI; } }
    public UnitListUI UnitListUI { get {return _unitListUI; } }
    public CharacterInfoUI CharacterInfoUI { get { return _characterInfoUI; } }
    public UnitLevelUpUI LevelUpUI { get { return _levelUpUI; } }
    public UnitLimitBreakUI LimitBreakUI {  get { return _limitBreakUI; } }
    private void Start()
    {
        Instance = this;
    }
}
