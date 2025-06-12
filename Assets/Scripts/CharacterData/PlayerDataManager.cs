using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField]private PlayerDataSO _playerDataSO;
    public PlayerDataSO PlayerDataSO { get { return _playerDataSO; } }
    [SerializeField]public PlayerData _playerData;

    public event EventHandler OnDataChange;

    private void Awake()
    {
        GameManager.Instance._resourceManager.OnLoadComplete += _resourceManager_OnLoadComplete;
    }

    private void _resourceManager_OnLoadComplete(object sender, System.EventArgs e)
    {
        _playerData=SaveLoadData.LoadCharacterData();
        SaveLoadData.ConvertToSO(_playerDataSO, _playerData);

    }
    public void SaveUnit()//call when lb, lu, acquire new char
    {
        _playerData.OwnedCharacter.Clear();
        for (int i = 0; i < _playerDataSO.OwnedCharacter.Count; i++)
        {
            CharacterModifyData cdata = new CharacterModifyData(_playerDataSO.OwnedCharacter[i].UnitID, _playerDataSO.OwnedCharacter[i].Level, _playerDataSO.OwnedCharacter[i].LimitBreak);
            _playerData.OwnedCharacter.Add(cdata);

        }
        OnDataChange?.Invoke(this, EventArgs.Empty);

        SaveLoadData.SaveCharacterData(_playerData);

    }
    public void  SaveItem()
    {

        _playerData.Items.Clear();
            for (int i = 0; i < _playerDataSO.Items.Count; i++)
            {
                SaveItemData it = new SaveItemData(_playerDataSO.Items[i].Material.ItemID, _playerDataSO.Items[i].Quantity);
            _playerData.Items.Add(it);
            }
            OnDataChange?.Invoke(this, EventArgs.Empty);
        SaveLoadData.SaveCharacterData(_playerData);
     

    }
    public void  SaveLineUp()
    {

        _playerData.LineUp.Clear();
        for (int i = 0; i < _playerDataSO.PlayerLineUp.Count; i++)
        {
            LineUpData lu = new(_playerDataSO.PlayerLineUp[i].Index, _playerDataSO.PlayerLineUp[i].Unit.UnitID, _playerDataSO.PlayerLineUp[i].SkillIndex);
            _playerData.LineUp.Add(lu);
        }
        OnDataChange?.Invoke(this, EventArgs.Empty);

        SaveLoadData.SaveCharacterData(_playerData);


    }
   
    
}
