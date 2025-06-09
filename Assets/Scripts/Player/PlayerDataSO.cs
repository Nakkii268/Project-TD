using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public string PlayerID;
    public string PlayerName;
    public List<ItemsData> Items;
    public List<AllianceUnit> OwnedCharacter;
    public List<LineUpSave> PlayerLineUp;
    public Progress PlayerProgress;
  

    public void ClearData()
    {
        PlayerID = string.Empty;
        PlayerName = string.Empty;
        Items.Clear();
        OwnedCharacter.Clear();
        PlayerLineUp.Clear();
        PlayerProgress = new Progress();
        
    }
    public int IsHaveItem(string id)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Material.ItemID == id) return Items[i].Quantity;
        }
        return 0;
    }
    public ItemsData GetItemById(string id)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Material.ItemID == id) return Items[i];
        }
        return null;
    }
    
    public void AddItem(Item item, int quantity)
    {
        if (IsHaveItem(item.ItemID) != 0)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Material.ItemID == item.ItemID) Items[i].Quantity+=quantity;
            }
        }
        else
        {
            Items.Add(new ItemsData(item, quantity));

        }
    }
    public List<AllianceUnit> GetLineUpUnit()
    {
        List<AllianceUnit> units = new List<AllianceUnit>();
        for (int i = 0; i < PlayerLineUp.Count; i++)
        {
            units.Add(PlayerLineUp[i].Unit);
        }
        return units;
    }
    public List<LineUpSave> GetLineUp() 
    {
        return PlayerLineUp;
    }
    public void UpdateLineUp(AllianceUnit unit, int indx,int skIndx)
    {
        
        //check if already have unit in this index:
        //yes=remove/update
        for (int i = 0; i < PlayerLineUp.Count; i++)
        {
            if (PlayerLineUp[i].Index == indx)
            {
                if (unit == null)
                {
                    PlayerLineUp.RemoveAt(i);
                    return;
                }
                else
                {
                    PlayerLineUp[i].Unit = unit;
                    return;
                }
            }
        }
        //no = add if not null
       if (unit != null)
        {
            PlayerLineUp.Add(new LineUpSave(indx, unit,skIndx));
        }
      
    }
    
    public bool IsLineUpContain(AllianceUnit unit)
    {
        HashSet<AllianceUnit> lineup = new HashSet<AllianceUnit>(GetLineUpUnit());
        if(lineup.Contains(unit)) return true;
        return false;
    }
    public void UpdateProgress()
    {
        if(PlayerProgress.Stage == (GameManager.Instance._resourceManager.GetChapterByIndex<ChapterSO>(PlayerProgress.ChapterIndex).StageQuantity-1))
        {
            PlayerProgress.ChapterIndex++;
            PlayerProgress.Stage = 0;
        }
        else
        {
            PlayerProgress.Stage++;
        }
    }
}
