using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public string PlayerID;
    public string PlayerName;
    public List<ItemsData> Items;
    public List<AllianceUnit> OwnedCharacter;
    public List<LineUpSave> PlayerLineUp;
    public Progress PlayerProgress;
    public Progress LastCompleteStage;

   public void ClearData()
    {
        PlayerID = string.Empty;
        PlayerName = string.Empty;  
        Items.Clear();
        OwnedCharacter.Clear();
        PlayerLineUp.Clear();
        PlayerProgress = new Progress();
        LastCompleteStage = new Progress();
    }
    public int IsHaveItem(string id)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Material.ItemID == id) return Items[i].Quantity;
        }
        return 0;
    }
    public ItemsData GetItem(string id)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Material.ItemID == id) return Items[i];
        }
        return null;
    }
}
