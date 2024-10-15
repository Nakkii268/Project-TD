using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private int MapSize_X=10;
    [SerializeField] private int MapSize_Y=10;
    [SerializeField] private GameObject[,] tiles;
    [SerializeField] private Vector2Int currentSelect;
    [SerializeField] private Camera Camera;
   
    [SerializeField] private InGameCharListUI charListUI;

    private void Awake()
    {
        instance = this;    
    }
    private void Start()
    {
        
        TilesSetup(MapSize_X, MapSize_Y);
    }

    private void Update()
    {
        
        
    }
    public void HandleRaycast()
    {
        RaycastHit info;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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

    private GameObject[] GetTileList()
    {
        GameObject[] list = new GameObject[transform.childCount];
        for(int i=0;i < list.Length; i++)
        {
            
                list[i] = transform.GetChild(i).gameObject;
            Debug.Log(i);
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
            tiles[currentSelect.x, currentSelect.y].GetComponent<Block>().DeloyUnit(data.unit,data.character);
            

        }
        foreach(var tile in ValidBlock(data.unit.GetAllianceType()))
        {
            tile.GetComponent<Block>().UnHighLightBlock();
        }
    }
    public List<GameObject> ValidBlock(string ut)
    {
        List<GameObject> list = new List<GameObject>();
        for(int x=0; x <5 ; x++)
        {
            for(int y=0; y < 5; y++)
            {
                if (tiles[x, y].GetComponent<Block>().IsDeloyable() && tiles[x, y].GetComponent<Block>().GetBlockType() == ut)
                {
                    list.Add(tiles[x, y]);
                }
                
            }
        }
        
        return list;
    }
}

