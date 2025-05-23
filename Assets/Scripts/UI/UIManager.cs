using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    Dictionary<System.Type, UICanvas> CanvasActives = new Dictionary<System.Type, UICanvas>();
    Dictionary<System.Type, UICanvas> CanvasPrefabs = new Dictionary<System.Type, UICanvas>();
    [SerializeField] private Transform ParentCanvas;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //load prefab from resources
        List<UICanvas> prefabs = GameManager.Instance._resourceManager.GetUIPrefab();
        for(int i = 0; i < prefabs.Count; i++)
        {
            CanvasPrefabs.Add(prefabs[i].GetType(), prefabs[i]);
            Debug.Log(prefabs[i].GetType());
        }
        OpenUI<MenuUI>();
    }
    public T OpenUI<T>() where T : UICanvas
    {
        T canvas = GetUI<T>();
        canvas.SetUp();
        Debug.Log(canvas.gameObject.activeSelf);
        canvas.gameObject.SetActive(true);
        Debug.Log(canvas.gameObject.activeSelf);
        Debug.Log(canvas.GetType());
        return canvas as T;
    }
    public T OpenUI<T>(AllianceUnit unit) where T : UICanvas
    {
        T canvas = GetUI<T>();
        canvas.SetUp(unit);
        canvas.gameObject.SetActive(true);

        return canvas as T;
    }
    public T OpenUI<T>(object para) where T : UICanvas
    {
        T canvas = GetUI<T>();
        canvas.SetUp(para);
        canvas.gameObject.SetActive(true);

        return canvas as T;
    }
    public void Close<T>(float time) where T : UICanvas
    {
        if (IsLoaded<T>())
        {
            CanvasActives[typeof(T)].Close(time);
        }
    }

    public bool IsLoaded<T>() where T : UICanvas
    {
        return CanvasActives.ContainsKey(typeof(T)) && CanvasActives[typeof(T)]!=null;
    }
    public bool IsOpened<T>() where T : UICanvas
    {
        return IsLoaded<T>() && CanvasActives[typeof(T)].gameObject.activeSelf;
    }
    public T GetUI<T>() where T : UICanvas
    {
        if (!IsLoaded<T>())
        {
            T prefab = GetUIPrefab<T>();
            T canvas = Instantiate(prefab,ParentCanvas);
            CanvasActives[typeof(T)] = canvas;
        }

        return CanvasActives[typeof(T)] as T;
    }
    public T GetUIPrefab<T>() where T : UICanvas
    {
        return CanvasPrefabs[typeof(T)] as T;
    } 
    //close all ui, except menu
    public void CloseToHome()
    {
        foreach(var canvas in CanvasActives)
        {
            if (canvas.Key == typeof(MenuUI)) continue;  
            if(canvas.Value!= null && canvas.Value.gameObject.activeSelf)
            {
                canvas.Value.Close(0);
            }
        }
    }
    //close all ui, no exception
    public void CloseAllUI()
    {
        foreach(var canvas in CanvasActives)
        {
             
            if(canvas.Value!= null && canvas.Value.gameObject.activeSelf)
            {
                canvas.Value.Close(0);
            }
        }
    }
    public void ToHomeMenu()
    {
        CloseToHome();
      
    }
}
