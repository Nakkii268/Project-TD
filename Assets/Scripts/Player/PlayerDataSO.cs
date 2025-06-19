using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public List<Progress> PlayerProgress;
    public List<ShopItemSave> ShopItemsData;

    public void ClearData()
    {
        PlayerID = string.Empty;
        PlayerName = string.Empty;
        Items.Clear();
        OwnedCharacter.Clear();
        PlayerLineUp.Clear();
        PlayerProgress = new List<Progress>();
        
    }

    //handle with item-begin
    public int IsHaveItem(string id)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Item.ItemID == id) return Items[i].Quantity;
        }
        return 0;
    }
    public ItemsData GetItemById(string id)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Item.ItemID == id) return Items[i];
        }
        return null;
    }
   
    public void AddItem(Item item, int quantity)
    {
        if (IsHaveItem(item.ItemID) != 0)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Item.ItemID == item.ItemID) Items[i].Quantity+=quantity;
            }
        }
        else
        {
            Items.Add(new ItemsData(item, quantity));

        }
    }
    public void RemoveItem(Item item, int quantity)
    {
        if (IsHaveItem(item.ItemID) != 0 )
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Item.ItemID == item.ItemID && Items[i].Quantity >= quantity) Items[i].Quantity-=quantity;
                if(Items[i].Quantity == 0)
                {
                    Items.Remove(Items[i]);
                }
            }
        }
        else
        {
            Debug.LogError("Some thing went wrong with item");

        }
    }
    //handle with item-end

    //handle line up
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

        Debug.Log(skIndx);
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
                    PlayerLineUp[i].SkillIndex = skIndx;
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

    //handle progress
    public void UpdateProgress(MapSO stage,int rating)
    {
        
        for (int i = 0; i < PlayerProgress.Count; i++) //loop through progress
        {
            if(stage.ChapterIndex == PlayerProgress[i].ChapterIndex) //if already have chapter - loop the stage list
            {
                for(int j = 0; j < PlayerProgress[i].StageList.Count; j++)
                {
                    if(stage.StageIndex == PlayerProgress[i].StageList[j].Stage) // if already passed the stage- check rating
                    {
                        if(PlayerProgress[i].StageList[j].Rating < rating)//better rating -> update rating
                        {
                            PlayerProgress[i].StageList[j].Rating = rating;
                        }
                        return;
                    }
                }
                PlayerProgress[i].StageList.Add(new StageData(stage.StageIndex,rating)); // dont have/ the new stage -> add stage and rating
                return;
            }

        }
        //dont have/ new chapter -> add new progress and add stage to it
        Progress newProgress = new Progress(stage.ChapterIndex); 
        newProgress.StageList.Add(new StageData(stage.StageIndex, rating));
        PlayerProgress.Add(newProgress);
    }
    public bool IsHaveChapter(int chapter)
    {
        for (int i = 0; i < PlayerProgress.Count; i++)
        {
            if (PlayerProgress[i].ChapterIndex == chapter) return true;
        }
        return false;
    }
    //handle unit
    public bool AddUnit(AllianceUnit unit)
    {
        for (int i = 0; i < OwnedCharacter.Count; i++)
        {
            if (unit.UnitID == OwnedCharacter[i].UnitID) return false;
        }
        OwnedCharacter.Add(unit);
        return true;    
    }
}
