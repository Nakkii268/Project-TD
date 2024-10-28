using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private bool Deloyable;
    [SerializeField] private AllianceUnit unit;
    [SerializeField] private Alliance unitDeloyed;
    [SerializeField] private BlockTag type;
    
    public bool IsHaveUnit()
    {
        if (unit == null) return false;
        return true;
    }
    public AllianceUnit GetUnit() { return unit; }
    public Alliance GetUnitDeloyed() { return unitDeloyed; }
    public Unit GetUnitAtBlock()
    {
        if (unit == null) return null;
        else return unit;
    }
    public bool IsDeloyable()
    {
        return Deloyable;
    }
    public void DeloyUnit(AllianceUnit u,GameObject g,int indx)
    {
        if (unit != null) return;
        unit = u;
        Deloyable = false;

        SpawnUnit(u,g,indx);
        UnHighLightBlock();
        unitDeloyed = GetComponentInChildren<Alliance>();
        Debug.Log("deloyed");
        
    }
    public void UnitReTreat()
    {
        if (unit == null) return;
        unit = null;
        Destroy(gameObject.GetComponentInChildren<Alliance>().gameObject);
        LevelManager.instance.GetLevelDPManager().DeploymentSlotFree();
        Deloyable = true;
        
       
    }
    public void SpawnUnit(AllianceUnit u,GameObject g, int indx)
    {
        GameObject unit = Instantiate(g,transform);
        unit.transform.position = GetStandPos();
        unit.GetComponent<Alliance>().SetUnit(u,new Vector2(transform.position.x, transform.position.y),indx);
        
    }
    public string GetBlockType() { 
       
        return type.ToString();    
    }
    public void HighLightBlock(int layer)
    {
        transform.GetChild(0).gameObject.layer = layer;
    }
    public void UnHighLightBlock()
    {
        transform.GetChild(0).gameObject.layer = 6;

    }
    private Vector3 GetStandPos()
    {
        return new Vector3(transform.position.x, transform.position.y - .25f, transform.position.z - .5f);
    }
}
[Serializable]

public enum BlockTag
{
    Ground,
    HighGround,
    BlueBox,
    RedBox,
    None
}