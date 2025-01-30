using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private int MapSize_X=10;
    [SerializeField] private int MapSize_Y=10;
    [SerializeField] private GameObject[,] tiles;
    [SerializeField] private MapSO map;
    public MapSO Map { get  { return map; } }

    [SerializeField] private Vector2Int currentSelect;
    [SerializeField] private CameraManager Camera;
    [SerializeField]private Quaternion cameraOriginRotate;
    [SerializeField] private InGameCharListUI charListUI;
    [SerializeField] private LevelDPManager levelDPManager;
    [SerializeField] private int currentUnitIndex;
    [SerializeField] private int deloymentLimit;
    [SerializeField] private int currentDeloyment = 0;
    //
    [SerializeField] private Transform unitUI;
    [SerializeField] private LevelLifePointManager levelLifePointManager;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private LevelStateMachineManager levelStateMachineManager;
    public LevelStateMachineManager LevelStateMachineManager { get { return levelStateMachineManager; } }


    public event EventHandler OnClickOtherTarget;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnPause;
    public event EventHandler<float> OnGameEnd;// win = 1; lose = 0; win but leaked =0.x

    private void Awake()
    {
        instance = this;
        Camera = CameraManager.instance;
        levelStateMachineManager = new LevelStateMachineManager(this);
        DisableComponent();
    }
    private void Start()
    {
        
        TilesSetup(MapSize_X, MapSize_Y);
        cameraOriginRotate = Camera.transform.rotation;
        levelStateMachineManager.ChangeState(levelStateMachineManager.LevelPrepareState);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (HandleRaycastOnUI()) return;
            HandleRaycast();
            if (currentSelect == -Vector2.one) return;
            if (!tiles[currentSelect.x, currentSelect.y].GetComponent<Block>().IsHaveUnit() )
            {
              
                OnClickOtherTarget?.Invoke(this, EventArgs.Empty);
                return;
               
            }else 
            {
                

                Camera.CamLookat(tiles[currentSelect.x, currentSelect.y].GetComponent<Block>().transform);
                currentUnitIndex = tiles[currentSelect.x, currentSelect.y].GetComponent<Block>().GetUnitDeloyed().GetUnitIndex();
                OnClickOtherTarget?.Invoke(this, EventArgs.Empty);
                tiles[currentSelect.x, currentSelect.y].GetComponent<Block>().GetUnitDeloyed().UIShowOnForcus();
                

            }
            
               
            
        }
        levelStateMachineManager.Update();
        
    }
    public void HandleRaycast()
    {
        RaycastHit info;
        Ray ray = Camera.GetCamera().ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Block")))
        {
            
            Vector2Int hitpos = LookupTileIndex(info.transform.gameObject);
            if (currentSelect == -Vector2.one)
            {
                currentSelect = hitpos;
                
               
            }
            if (currentSelect != hitpos)
            {
               
                currentSelect = hitpos;
                
                
            }
            

        }
        else
        {
            currentSelect = -Vector2Int.one;
        }
        
       
    }
    public bool  HandleRaycastOnUI()
    {

        RaycastHit info;
        Ray ray = Camera.GetCamera().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("UnitUI")))
        {
            
            if(info.transform.gameObject.layer == 10)
            {
                return true;
            }

        }
        return false;
        
    }
    public LevelDPManager GetLevelDPManager()
    {
        return levelDPManager;
    }

    private GameObject[] GetTileList()
    {
        GameObject[] list = new GameObject[transform.childCount];
        for(int i=0;i < list.Length; i++)
        {
            
                list[i] = transform.GetChild(i).gameObject;
            
        }
        return list;
    }
    private Vector2Int NameConvert(string name)
    {
        string[] posValue = name.Split(",");
        Vector2Int pos = new Vector2Int(int.Parse(posValue[0]), int.Parse(posValue[1]));
        return pos;
    }
    private void TilesSetup(int x,int y)
    {
        tiles = new GameObject[x, y];
        GameObject[] tileList = GetTileList();
        for (int i = 0; i < tileList.Length; i++)
        {
            Vector2Int value = NameConvert(tileList[i].name);
            tiles[value.x,value.y] = tileList[i];
        }
    }
    private Vector2Int LookupTileIndex(GameObject info)
    {
        for (int i = 0; i < MapSize_X; i++)
        {
            for (int j = 0; j < MapSize_Y; j++)
            {
                if (tiles[i, j] == info) return new Vector2Int(i, j);
            }
        }
       
        return -Vector2Int.one; 
    }
    public void HanlderOnCharDrop(CharacterData data)
    {
        if(currentSelect == -Vector2.one) return;
        if (tiles[currentSelect.x, currentSelect.y].GetComponent<Block>().IsDeloyable() && tiles[currentSelect.x, currentSelect.y].GetComponent<Block>().GetBlockType() == data.unit.GetAllianceType())
        {
            tiles[currentSelect.x, currentSelect.y].GetComponent<Block>().DeloyUnit(data.unit,data.character,data.charIndex);
            

        }
        foreach(var tile in ValidBlock(data.unit.GetAllianceType()))
        {
            tile.GetComponent<Block>().UnHighLightBlock();
        }
    }
    public List<GameObject> ValidBlock(string ut)
    {
        List<GameObject> list = new List<GameObject>();
        for(int x=0; x <MapSize_X ; x++)
        {
            for(int y=0; y < MapSize_Y; y++)
            {
                if (tiles[x, y].GetComponent<Block>().IsDeloyable() && tiles[x, y].GetComponent<Block>().GetBlockType() == ut)
                {
                    list.Add(tiles[x, y]);
                }
                
            }
        }
        
        return list;
    }
    public void HighLightBlockList(List<Vector2> list,int layer)
    {
        if (list == null) return;
        foreach (var tile in list)
        {
            if (IsTileInRange(Vector2Int.RoundToInt(tile)))
            {
                tiles[Vector2Int.RoundToInt(tile).x, Vector2Int.RoundToInt(tile).y].GetComponent<Block>().HighLightBlock(layer);
                
            }
            
        }
    }
    public void UnHighLightBlockList(List<Vector2> list)
    {
        if (list == null) return;
        foreach (var tile in list)
        {
            if (IsTileInRange(Vector2Int.RoundToInt(tile)))
            {
                tiles[Vector2Int.RoundToInt(tile).x, Vector2Int.RoundToInt(tile).y].GetComponent<Block>().UnHighLightBlock();

            }

        }
    }
    private bool IsTileInRange(Vector2Int tile)
    {
        if (tile.x >=0 && tile.x < MapSize_X && tile.y >= 0 && tile.y < MapSize_Y)
        {
            return true;
        }
        return false;
    }
    public void GameEnd(float endState)
    {
        OnGameEnd?.Invoke(this, endState);
    }

    public void DisableComponent()
    {
        levelDPManager.enabled = false; 
        waveManager.enabled = false;
    }
    public void EnableComponent()
    {
        levelDPManager.enabled = true;
        waveManager.enabled = true;
    }
}

