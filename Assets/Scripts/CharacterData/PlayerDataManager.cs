using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField]private InGameUnit AllianceUnitsList;
    [SerializeField]private Dictionary<string , AllianceUnit> UnitsDictionary;
    [SerializeField]private Dictionary<string , AllianceUnit> PlayerUnitsDictionary;
   
    [SerializeField]private PlayerData playerData;

    private void Awake()
    {
        UnitsDictionary = new Dictionary<string , AllianceUnit>();
        for (int i = 0; i < AllianceUnitsList.Units.Count; i++)
        {
            UnitsDictionary.Add( AllianceUnitsList.Units[i].UnitID, AllianceUnitsList.Units[i]);
        }
    }
    public AllianceUnit GetSOByID(string id)
    {
        return UnitsDictionary.ContainsKey(id)? UnitsDictionary[id] : null;
    }

    private void SaveData()
    {
       
        
        SaveLoadData.SaveCharacterData(playerData);
    }

    private void LoadData()
    {
        PlayerUnitsDictionary = new Dictionary<string, AllianceUnit>();
        PlayerData data = SaveLoadData.LoadCharacterData();
        playerData= data;   
        if (data.OwnedCharacter == null) return;
        for (int i = 0; i < data.OwnedCharacter.Count; i++)
        {
            AllianceUnit unit = GetSOByID(data.OwnedCharacter[i].Id);
            
            unit.Level = data.OwnedCharacter[i].Level;
            unit.LimitBreak = data.OwnedCharacter[i].LimitBreak;
            
            PlayerUnitsDictionary.Add(unit.UnitID,unit);
        }


    }

    
 /*   public void Test()
    {
        AllianceUnit unit = GetSOByID("001");
        Debug.Log(unit.Block);
        Debug.Log(unit.Attack);
        Debug.Log(unit.AttackInterval);
        unit.Level = 20;
        unit.Attack = 100000;
        Debug.Log(unit.AttackRange);
        Debug.Log(unit.DamageType);
        Debug.Log(unit.UnitID);
        Debug.Log(unit.Level);
        Debug.Log(unit.LimitBreak);
        Debug.Log(unit.RedeployTime);
        Debug.Log(unit.Resistance);
        unist = unit;

    }*///work kinda good
}
