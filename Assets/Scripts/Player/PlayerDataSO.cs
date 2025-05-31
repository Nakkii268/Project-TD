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
    public ItemsData GetItem(string id)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Material.ItemID == id) return Items[i];
        }
        return null;
    }

    public List<AllianceUnit> GetLineUp()
    {
        List<AllianceUnit> units = new List<AllianceUnit>();
        for (int i = 0; i < PlayerLineUp.Count; i++)
        {
            units.Add(PlayerLineUp[i].Unit);
        }
        return units;
    }
    public void UpdateLineUp(AllianceUnit unit, int indx)
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
           

            PlayerLineUp.Add(new LineUpSave(indx, unit));
            
        }
    }
    public bool IsLineUpContain(AllianceUnit unit)
    {
        HashSet<AllianceUnit> lineup = new HashSet<AllianceUnit>(GetLineUp());
        if(lineup.Contains(unit)) return true;
        return false;
    }
}
