using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/ConsumableItem/CharacterAcquisition")]
public class CharacterAcquisition : ConsumableItem
{
    public AllianceUnit UnitAcquire;
    public override void OnUse()
    {
        if (GameManager.Instance._playerDataManager.PlayerDataSO.AddUnit(UnitAcquire))
        {
            GameManager.Instance._playerDataManager.PlayerDataSO.RemoveItem(this, 1);
            GameManager.Instance._playerDataManager.SaveItem();
            GameManager.Instance._playerDataManager.SaveUnit();
        }
    }
}
