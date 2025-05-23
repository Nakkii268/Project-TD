using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField]private PlayerDataSO _playerDataSO;
    public PlayerDataSO PlayerDataSO { get { return _playerDataSO; } }
    [SerializeField]private PlayerData _playerData;
    public PlayerData PlayerData { get { return _playerData; } }


    private void Start()
    {
        GameManager.Instance._resourceManager.OnLoadComplete += _resourceManager_OnLoadComplete;
    }

    private void _resourceManager_OnLoadComplete(object sender, System.EventArgs e)
    {
        SaveLoadData.ConvertToSO(_playerDataSO, _playerData);

    }
}
