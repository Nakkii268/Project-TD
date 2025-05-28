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
                Debug.Log("that him!");
                if (unit == null)
                {
                    Debug.Log("null case");
                    PlayerLineUp.RemoveAt(i);
                    return;
                }
                else
                {
                    Debug.LogWarning("add1");
                    PlayerLineUp[i].Unit = unit;
                    return;
                }
                
            }
            
        }
        //no = add if not null
       if (unit != null)
        {
            Debug.LogWarning("add2");

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
