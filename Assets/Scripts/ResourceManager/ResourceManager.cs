using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEditor.Progress;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<string,Item> ItemSOData = new Dictionary<string,Item>();
    private Dictionary<string,AllianceUnit> UnitSOData = new Dictionary<string,AllianceUnit>();
    private Dictionary<string,ChapterSO> ChapterSOData = new Dictionary<string,ChapterSO>();
    [SerializeField] private ChapterSO test;
    private void Start()
    {
        LoadAllChapter<ChapterSO>("Chapter");
        LoadAllItem<Item>("Item");
        LoadAllUnit<AllianceUnit>("Unit");
    }

    private void LoadAllItem<T>(string label) where T : Item
    {
        
        Addressables.LoadAssetsAsync<T>(label,null).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded) {
                
                List<T> list = handle.Result as List<T>;
                foreach (T item in list)
                {
                    if (!ItemSOData.ContainsKey(item.ItemID))
                    {
                        ItemSOData.Add(item.ItemID, item);
                        Debug.Log(item);

                    }
                }
            }
            else
            {
                Debug.Log("Loading item failed");
            }
        };
    }
    private void LoadAllUnit<T>(string label) where T : AllianceUnit
    {
        Addressables.LoadAssetsAsync<T>(label, null).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded) {
                List<T> list = handle.Result as List<T>;

                foreach (T unit in list)
                {
                    if (!UnitSOData.ContainsKey(unit.UnitID))
                    {
                        UnitSOData.Add(unit.UnitID , unit);
                        Debug.Log(unit);

                    }
                }
            }
            else
            {
                Debug.Log("Loading item failed");
            }
        };
    }
    private void LoadAllChapter<T>(string label) where T : ChapterSO
    {
        Addressables.LoadAssetsAsync<T>(label, null).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded) {
                List<T> list = handle.Result as List<T>;

                foreach (T chap in list)
                {
                    if (!ChapterSOData.ContainsKey(chap.ChapterID))
                    {
                        ChapterSOData.Add(chap.ChapterID, chap);
                        Debug.Log(chap);

                    }
                }
                test = ChapterSOData["C001"];
            }
            else
            {
                Debug.Log("Loading item failed");
            }
        };
    }

    public T GetItemById<T>(string id) where T: Item
    {
        if (ItemSOData.ContainsKey(id))
        {
            return ItemSOData[id] as T;
        }
        return null;
    }
    public T GetUnitById<T>(string id) where T: AllianceUnit
    {
        if (UnitSOData.ContainsKey(id))
        {
            return UnitSOData[id] as T;
        }
        return null;
    }
    public T GetChapterById<T>(string id) where T: ChapterSO
    {
        if (ChapterSOData.ContainsKey(id))
        {
            return ChapterSOData[id] as T;
        }
        return null;
    }
}
