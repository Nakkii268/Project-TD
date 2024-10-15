using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private bool Deloyable;
    [SerializeField] private AllianceUnit unit;
    [SerializeField] private BlockTag type;
    

    public Unit GetUnitAtBlock()
    {
        if (unit == null) return null;
        else return unit;
    }
    public bool IsDeloyable()
    {
        return Deloyable;
    }
    public void DeloyUnit(AllianceUnit u,GameObject g)
    {
        if (unit != null) return;
        unit = u;
        Deloyable = false;
        SpawnUnit(u,g);
        UnHighLightBlock();
        
    }
    public void UnitReTreat()
    {
        if (unit == null) return;
        unit = null;
        Destroy(gameObject.transform.GetChild(0).gameObject);
        Deloyable = true;
    }
    public void SpawnUnit(AllianceUnit u,GameObject g)
    {
        GameObject unit = Instantiate(g,transform);
        unit.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        unit.GetComponent<Alliance>().SetUnit(u,new Vector2(transform.position.x, transform.position.y));
    }
    public string GetBlockType() { 
       
        return type.ToString();    
    }
    public void HighLightBlock()
    {
        transform.GetChild(0).gameObject.layer = 7;
    }
    public void UnHighLightBlock()
    {
        transform.GetChild(0).gameObject.layer = 6;

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