using System;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class ResourceManager : MonoBehaviour
{
    private Dictionary<string,Item> ItemSOData = new Dictionary<string,Item>();
    private Dictionary<string,AllianceUnit> UnitSOData = new Dictionary<string,AllianceUnit>();
    private Dictionary<string,ChapterSO> ChapterSOData = new Dictionary<string,ChapterSO>();
    private List<UICanvas> UIPrefab = new List<UICanvas>();
   
    [SerializeField] private bool isItemDone;
    [SerializeField] private bool isUnitDone;
    [SerializeField] private bool isChapterDone;
    [SerializeField] private bool isUIDone;
    public event EventHandler OnLoadComplete;
    private void Start()
    {
        LoadAllChapter<ChapterSO>("Chapter");
        LoadAllUnit<AllianceUnit>("Unit");
        LoadAllItem<Item>("Item");
        LoadAllUICanvas<UICanvas>("UIPrefab");
    }
    public  void LoadAllUICanvas<T>(string label) where T : UICanvas
    {
        
        Addressables.LoadAssetsAsync<GameObject>(label,null).Completed += handle =>
        {
            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var ui in handle.Result ) {
                    
                    UIPrefab.Add(ui.GetComponent<UICanvas>());
                }
                isUIDone = true;
                AllSOLoaded();
            }
        };
        
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
                       

                    }
                }
                isItemDone = true;
                AllSOLoaded();
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
                       

                    }
                }
                isUnitDone = true;
                AllSOLoaded();
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
                      

                    }
                }
                isChapterDone = true;
                AllSOLoaded();
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
    public List<UICanvas> GetUIPrefab()
    {
        return UIPrefab;
    }
    private void AllSOLoaded()
    {
        if(isItemDone && isChapterDone && isUnitDone && isUIDone)
        {
            Debug.Log("Done!");
            OnLoadComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}
